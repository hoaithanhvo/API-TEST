using AutoMapper;
using Azure.Identity;
using MCSAndroidAPI.Constants;
using MCSAndroidAPI.Contracts;
using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;
using MCSAndroidAPI.Utility;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Diagnostics;

namespace MCSAndroidAPI.Repositories
{
    public class LotScrapRepository : RepositoryBase<TLotScrap>, ILotScrapRepository
    {

        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public LotScrapRepository(NidecMCSContext nidecMCSContext, IMapper mapper, ILogger logger) : base(nidecMCSContext, mapper)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResponseModel<object>> CreateAsync(LotScrapModel model, string token)
        {
            var response = new ResponseModel<object>();

            try
            {
                var lotScrap = await NidecMCSContext.TLotScraps.Where(x => x.DivisionCd == model.DivisionCd && x.ProcessCd == model.ProcessCd &&
                    x.MaterialCd == model.ProductNo && x.LotNo == model.LotNo)
                    .OrderByDescending(x => x.ReportId).FirstOrDefaultAsync();

                decimal reportId = 1;
                if (lotScrap != null)
                {
                    reportId = lotScrap.ReportId + 1;
                }

                var itemName = (from m in NidecMCSContext.AMaterials where m.MaterialCd == model.ItemNo select m.MaterialName).FirstOrDefault();

                // get the username using the access token
                var principal = Validation.ValidateToken(token);
                var username = principal?.Identity?.Name;

                var item = _mapper.Map<TLotScrap>(model);
                item.ReportId = reportId;
                item.ItemName = itemName;
                item.InputDiv = 0;
                item.ReportTime = DateTime.Now;
                item.CreateDate = DateTime.Now;
                item.CreatedBy = username;

                this.Create(item);

                Generation.GenerateResponse(ref response, new {reportId = item.ReportId});

            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return response;
        }

        public async Task<ResponseModel<List<LotScrapModel>>> GetItemsAsync(LotScrapModel model)
        {
            var response = new ResponseModel<List<LotScrapModel>>();

            try
            {
                var querydata = await (from ls in NidecMCSContext.TLotScraps
                                 join sr in NidecMCSContext.MScrapReasons on new { A = ls.ScrapRsnCd, B = ls.ProcessCd, C = ls.DivisionCd }
                                 equals new { A = sr.ScrapRsnCd, B = sr.ProcessCd, C = sr.DivisionCd }
                                 join mm in NidecMCSContext.AMaterials on ls.ItemNo equals mm.MaterialCd
                                 where (ls.DivisionCd == model.DivisionCd && ls.ProcessCd == model.ProcessCd &&
                                    ls.MaterialCd == model.ProductNo && ls.LotNo == model.LotNo)
                                 select new { ls.LotNo, ls.ReportId, ls.ReportTime, mm.MaterialType, ls.ItemNo, 
                                     ls.ScrapQty, sr.ScrapRsnCd, sr.ScrapRsnName, ls.UnitPrice, ls.ScrapAmt, ls.ScrapNote })
                                 .OrderByDescending(x => x.MaterialType).ThenByDescending(x => x.ReportId).ToListAsync();

                var models = new List<LotScrapModel>();
                for (int i = 0; i < querydata.Count; i++)
                {
                    decimal qty = 0;
                    decimal price = 0;
                    decimal amount = 0;
                    decimal.TryParse(querydata[i].ScrapQty.ToString(), out qty);
                    decimal.TryParse(querydata[i].UnitPrice.ToString(), out price);
                    decimal.TryParse(querydata[i].ScrapAmt.ToString(), out amount);

                    models.Add(new LotScrapModel
                    {
                        LotNo = querydata[i].LotNo,
                        ReportId = querydata[i].ReportId,
                        ItemType = querydata[i].MaterialType,
                        ItemNo = querydata[i].ItemNo,
                        ScrapQty = qty,
                        ScrapRsnCd = querydata[i].ScrapRsnCd,
                        ScrapRsnName = querydata[i].ScrapRsnName,
                        ScrapNote = querydata[i].ScrapNote,
                        UnitPrice = price,
                        ScrapAmt = amount,
                        DivisionCd = model.DivisionCd,
                        ProcessCd = model.ProcessCd,
                        ProductNo = model.ProductNo,
                    });
                }

                _logger.LogInformation($"[GetItems] Count: {models.Count}");

                Generation.GenerateResponse(ref response, models);

            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return response;
        }

        public async Task<ResponseModel<List<string>>> GetListItemNoAsync(string productNo, string type)
        {
            var response = new ResponseModel<List<string>>();

            try
            {
                var listItemNo = await (from bm in NidecMCSContext.MBomMaterials
                                 join mt in NidecMCSContext.AMaterials on bm.MaterialNo equals mt.MaterialCd
                                 where (bm.ModelNo == productNo && mt.MaterialType == type)
                                 group mt by bm.MaterialNo into c
                                 select c.Key).ToListAsync();

                _logger.LogInformation($"[GetListItemNo] Count: {listItemNo.Count}");
                Generation.GenerateResponse(ref response, listItemNo);

            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false );
            }

            return response;
        }

        public async Task<ResponseModel<object>> GetTypesAndReasonsAsync(DivisionCdAndProcessCdModel model, string productNo)
        {
            var response = new ResponseModel<object>();

            try
            {
                var types = await (from am in NidecMCSContext.AMaterials
                                   join bm in NidecMCSContext.MBomMaterials on am.MaterialCd equals bm.MaterialNo
                                   where bm.ModelNo == productNo
                                   group am by am.MaterialType into c
                                   select c.Key).ToListAsync();

                var reasons = await NidecMCSContext.MScrapReasons.Where(x => x.DivisionCd == model.DivisionCd && x.ProcessCd == model.ProcessCd)
                    .Select(s => _mapper.Map<ScrapReasonModel>(s)).ToListAsync();

                var data = new { types, reasons };

                _logger.LogInformation($"[GetTypesAndReasons]\ntypes: count = {data.types.Count}\nreasons: count = {data.reasons.Count}");

                Generation.GenerateResponse(ref response, data);

            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return response;
        }

        public async Task<ResponseModel<object>> UpdateAsync(LotScrapModel model, string token)
        {
            var response = new ResponseModel<object>();

            try
            {
                var item = await this.FindByCondition(x => x.DivisionCd == model.DivisionCd && x.ProcessCd == model.ProcessCd &&
                    x.MaterialCd == model.ProductNo && x.LotNo == model.LotNo && x.ReportId == model.ReportId).FirstOrDefaultAsync();

                if (item == null)
                {
                    _logger.LogWarning($"[Update] {SystemConstants.Message.NOT_FOUND}");
                    Generation.GenerateResponse(ref response, null, false, SystemConstants.Message.NOT_FOUND);
                }
                else
                {
                    // get the username using the access token
                    var principal = Validation.ValidateToken(token);
                    var username = principal?.Identity?.Name;

                    var itemName = (from m in NidecMCSContext.AMaterials where m.MaterialCd == model.ItemNo select m.MaterialName).FirstOrDefault();

                    item.ItemNo = model.ItemNo;
                    item.ScrapQty = model.ScrapQty;
                    item.UnitPrice = model.UnitPrice;
                    item.ScrapAmt = model.ScrapAmt;
                    item.ScrapRsnCd = model.ScrapRsnCd;
                    item.ScrapNote = model.ScrapNote;
                    item.InputDiv = 0;
                    item.UpdateDate = DateTime.Now;
                    item.UpdatedBy = username;
                    item.ItemName = itemName;

                    this.Update(item);

                    Generation.GenerateResponse(ref response, null);
                }

            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return response;
        }
    }
}
