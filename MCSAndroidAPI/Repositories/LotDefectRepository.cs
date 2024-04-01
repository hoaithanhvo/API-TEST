using AutoMapper;
using Azure;
using MCSAndroidAPI.Constants;
using MCSAndroidAPI.Contracts;
using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;
using MCSAndroidAPI.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace MCSAndroidAPI.Repositories
{
    public class LotDefectRepository : RepositoryBase<TLotDefect>, ILotDefectRepository
    {
        private ICommonRepository _commonRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public LotDefectRepository(NidecMCSContext nidecMCSContext, IMapper mapper, ILogger logger) : base(nidecMCSContext, mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _commonRepository = new CommonRepository(nidecMCSContext, mapper, _logger);
        }

        public async Task<ResponseModel<object>> CreateAsync(LotDefectModel model, string token)
        {
            var response = new ResponseModel<object>();

            try
            {
                var lotDefectResult = await this.FindByCondition(x => x.DivisionCd == model.DivisionCd && x.ProcessCd == model.ProcessCd &&
                    x.MaterialCd == model.ProductNo && x.LotNo == model.LotNo).OrderByDescending(x => x.ReportId).FirstOrDefaultAsync();

                decimal reportId = 1;
                if (lotDefectResult != null)
                {
                    reportId = lotDefectResult.ReportId + 1;
                }

                // get the username using the access token
                var principal = Validation.ValidateToken(token);
                var username = principal?.Identity?.Name;

                var lotDefect = this.Mapper.Map<TLotDefect>(model);
                lotDefect.ReportId = reportId;
                lotDefect.CreateDate = DateTime.Now;
                lotDefect.CreatedBy = username;

                this.Create(lotDefect);

                await _commonRepository.SetTriggerAsync(model, username);

                Generation.GenerateResponse(ref response, new {reportId = lotDefect.ReportId});

            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false, ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel<List<LotDefectModel>>> GetItemsAsync(LotDefectModel model)
        {

            var response = new ResponseModel<List<LotDefectModel>>();

            try
            {
                var querydata = await (from ld in NidecMCSContext.TLotDefects
                                       join dr in NidecMCSContext.MDefectReasons
                                       on new { A = ld.DefectRsnCd, B = ld.ProcessCd, C = ld.DivisionCd }
                                       equals new { A = dr.DefectRsnCd, B = dr.ProcessCd, C = dr.DivisionCd }
                                       where (ld.DivisionCd == model.DivisionCd && ld.ProcessCd == model.ProcessCd && ld.MaterialCd == model.ProductNo && ld.LotNo == model.LotNo)
                                       select new { ld.DivisionCd, ld.ProcessCd, ld.MaterialCd, ld.LotNo, ld.ReportId, ld.ReportTime, ld.DefectQty, dr.DefectRsnName, ld.DefectNote, dr.DefectRsnCd })
                                       .OrderByDescending(x => x.ReportId).ToListAsync();


                var lotDefects = new List<LotDefectModel>();
                for (int i = 0; i < querydata.Count; i++)
                {
                    var defect = new LotDefectModel
                    {
                        DivisionCd = querydata[i].DivisionCd,
                        ProcessCd = querydata[i].ProcessCd,
                        ProductNo  = querydata[i].MaterialCd,
                        LotNo = querydata[i].LotNo,
                        ReportId = querydata[i].ReportId,
                        ReportTime = querydata[i].ReportTime?? DateTime.Now,
                        DefectQty = querydata[i].DefectQty?? 0,
                        DefectRsnName = querydata[i].DefectRsnName,
                        DefectRsnCd = querydata[i].DefectRsnCd,
                        DefectNote = querydata[i].DefectNote,
                    };

                    lotDefects.Add(defect);
                }

                _logger.LogInformation($"[GetItems] Count: {lotDefects.Count}");

                Generation.GenerateResponse(ref response, lotDefects);

            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false, ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel<List<DefectReasonModel>>> GetReasonsAsync(DivisionCdAndProcessCdModel model)
        {
            var response = new ResponseModel<List<DefectReasonModel>>();

            try
            {
                response = await _commonRepository.GetDefectReasonsAsync(model.DivisionCd, model.ProcessCd);

            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return response;
        }

        public async Task<ResponseModel<object>> UpdateAsync(LotDefectModel model, string token)
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

                    item.ReportTime = model.ReportTime;
                    item.DefectQty = model.DefectQty;
                    item.DefectRsnCd = model.DefectRsnCd;
                    item.DefectNote = model.DefectNote;
                    item.UpdateDate = DateTime.Now;
                    item.UpdatedBy = username;

                    this.Update(item);

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

        public async Task<ResponseModel<object>> DeleteAsync(LotDefectModel model, string token)
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
    }
}
