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
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MCSAndroidAPI.Repositories
{
    public class LotStopRepository : RepositoryBase<TLotStop>, ILotStopRepository
    {
        private ICommonRepository _commonRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public LotStopRepository(NidecMCSContext nidecMCSContext, IMapper mapper, ILogger logger) : base(nidecMCSContext, mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _commonRepository = new CommonRepository(nidecMCSContext, mapper, _logger);
        }

        public async Task<ResponseModel<object>> CreateAsync(LotStopModel model, string token)
        {
            var response = new ResponseModel<object>();
            try
            {
                var lotStop = await NidecMCSContext.TLotStops.Where(x => x.DivisionCd == model.DivisionCd && x.ProcessCd == model.ProcessCd &&
                    x.MaterialCd == model.ProductNo && x.LotNo == model.LotNo)
                    .OrderByDescending(x => x.ReportId)
                    .FirstOrDefaultAsync();

                decimal reportId = 1;
                if (lotStop != null)
                {
                    reportId = lotStop.ReportId + 1;
                }

                // get the username using the access token
                var principal = Validation.ValidateToken(token);
                var username = principal?.Identity?.Name;

                var item = this.Mapper.Map<TLotStop>(model);
                item.ReportId = reportId;
                item.InputDiv = 0;
                item.CreateDate = DateTime.Now;
                item.CreatedBy = username;

                this.Create(item);

                await _commonRepository.SetTriggerAsync(model, username);

                Generation.GenerateResponse(ref response, new { reportId = item.ReportId });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return response;
        }

        public async Task<ResponseModel<object>> DeleteAsync(LotStopModel model, string token)
        {
            var response = new ResponseModel<object>();
            try
            {
                var item = await this.FindByCondition(x => x.DivisionCd == model.DivisionCd && x.ProcessCd == model.ProcessCd &&
                    x.MaterialCd == model.ProductNo && x.LotNo == model.LotNo && x.ReportId == model.ReportId).FirstOrDefaultAsync();

                if (item == null)
                {
                    _logger.LogWarning($"[Delete] {SystemConstants.Message.NOT_FOUND}");
                    Generation.GenerateResponse(ref response, null, false, SystemConstants.Message.NOT_FOUND);
                }
                else
                {
                    this.Delete(item);

                    // get the username using the access token
                    var principal = Validation.ValidateToken(token);
                    var username = principal?.Identity?.Name;

                    await _commonRepository.SetTriggerAsync(model, username);

                    Generation.GenerateResponse(ref response, null);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return response;
        }

        public async Task<ResponseModel<List<LotStopModel>>> GetItemsAsync(LotStopModel model)
        {
            var response = new ResponseModel<List<LotStopModel>>();

            try
            {
                var querydata = await (from ls in NidecMCSContext.TLotStops
                                       join sr in NidecMCSContext.MStopReasons
                                       on new { A = ls.StopRsnCd, B = ls.ProcessCd }
                                       equals new { A = sr.StopRsnCd, B = sr.ProcessCd }
                                       where (ls.DivisionCd == model.DivisionCd && ls.ProcessCd == model.ProcessCd && ls.MaterialCd == model.ProductNo && ls.LotNo == model.LotNo)
                                       select new { ls.LotNo, ls.ReportId, ls.StopStrTime, ls.StopEndTime, ls.StopHr, ls.StopRsnCd, sr.StopRsnName, ls.StopNote })
                                       .OrderByDescending(x => x.ReportId).ToListAsync();

                var stop_list = new List<LotStopModel>();
                for (int i = 0; i < querydata.Count; i++)
                {
                    decimal stopInterval = 0;
                    decimal.TryParse(querydata[i].StopHr.ToString(), out stopInterval);

                    stop_list.Add(new LotStopModel
                    {
                        LotNo = querydata[i].LotNo,
                        ReportId = querydata[i].ReportId,
                        StopStrTime = querydata[i].StopStrTime,
                        StopEndTime = querydata[i].StopEndTime,
                        StopHr = stopInterval,
                        StopRsnCd = querydata[i].StopRsnCd,
                        StopRsnName = querydata[i].StopRsnName,
                        StopNote = querydata[i].StopNote,
                        DivisionCd = model.DivisionCd,
                        ProcessCd = model.ProcessCd,
                        ProductNo = model.ProductNo,
                    });
                }

                _logger.LogInformation($"[GetItems]  Count: {stop_list.Count}");
                Generation.GenerateResponse(ref response, stop_list);
            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return response;
        }

        public async Task<ResponseModel<List<StopReasonModel>>> GetReasonsAsync(DivisionCdAndProcessCdModel model)
        {
            var response = new ResponseModel<List<StopReasonModel>>();

            try
            {
                var models = await NidecMCSContext.MStopReasons.Where(x => x.DivisionCd == model.DivisionCd && x.ProcessCd == model.ProcessCd)
                    .Select(s => _mapper.Map<StopReasonModel>(s)).ToListAsync();

                _logger.LogInformation($"[GetReasons] count: {models.Count}");

                Generation.GenerateResponse(ref response, models);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false );
            }

            return response;
        }

        public async Task<ResponseModel<object>> UpdateAsync(LotStopModel model, string token)
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

                    item.StopStrTime = model.StopStrTime;
                    item.StopEndTime = model.StopEndTime;
                    item.StopHr = model.StopHr;
                    item.StopRsnCd = model.StopRsnCd;
                    item.StopNote = model.StopNote;
                    item.InputDiv = 0;
                    item.UpdateDate = DateTime.Now;
                    item.UpdatedBy = username;

                    this.Update(item);

                    await _commonRepository.SetTriggerAsync(model, username);

                    response.status = SystemConstants.Status.SUCCESS;
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
