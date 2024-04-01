using AutoMapper;
using Azure;
using Azure.Core;
using MCSAndroidAPI.Constants;
using MCSAndroidAPI.Contracts;
using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;
using MCSAndroidAPI.Utility;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Principal;

namespace MCSAndroidAPI.Repositories
{
    public class LotManRepository : RepositoryBase<TLotWorker>, ILotManRepository
    {
        private ICommonRepository _commonRepository;
        private readonly ILogger _logger;
        public LotManRepository(NidecMCSContext nidecMCSContext, IMapper mapper, ILogger logger) : base(nidecMCSContext, mapper)
        {
            _logger = logger;
            _commonRepository = new CommonRepository(nidecMCSContext, mapper, _logger);
        }

        public async Task<ResponseModel<List<ProcessOpeModel>>> GetJobsAsync(DivisionCdAndProcessCdModel model)
        {
            return await _commonRepository.GetJobsAsync(model);
        }

        public async Task<ResponseModel<object>> CreateAsync(LotManModel model, string token)
        {
            var response = new ResponseModel<object>();
            try
            {
                var lotMan = await this.FindByCondition(m => m.DivisionCd == model.DivisionCd && m.ProcessCd == model.ProcessCd &&
                    m.MaterialCd == model.ProductNo && m.LotNo == model.LotNo).OrderByDescending(m => m.ReportId).FirstOrDefaultAsync();

                decimal reportId = 1;
                if (lotMan != null)
                {
                    reportId = lotMan.ReportId + 1;
                }

                // get the username using the access token
                var principal = Validation.ValidateToken(token);
                var username = principal?.Identity?.Name;

                var item = this.Mapper.Map<TLotWorker>(model);
                item.InputDiv = "0";
                item.ReportTime = DateTime.Now;
                item.CreateDate = DateTime.Now;
                item.CreatedBy = username;
                item.ReportId = reportId;


                this.Create(item);
                Generation.GenerateResponse(ref response, new { reportId = item.ReportId });

            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return response;
        }

        public async Task<ResponseModel<object>> UpdateAsync(LotManModel model, string token)
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

                    item.WorkerCd = model.WorkerCd;
                    item.JobCd = model.JobCd;
                    item.MachineCd = model.MachineCd;
                    item.LotWorkerNote = model.LotWorkerNote;
                    item.UpdateDate = DateTime.Now;
                    item.UpdatedBy = username;

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

        public async Task<ResponseModel<List<LotManModel>>> GetItemsAsync(LotManModel model)
        {
            var response = new ResponseModel<List<LotManModel>>();

            try
            {
                var queryList = await (from lpo in NidecMCSContext.TLotWorkers
                                        join mpo in NidecMCSContext.MProcessOpes
                                        on new { A = lpo.DivisionCd, B = lpo.ProcessCd, C = lpo.JobCd } equals new { A = mpo.DivisionCd, B = mpo.ProcessCd, C = mpo.OperationCd }
                                        where lpo.DivisionCd == model.DivisionCd && lpo.ProcessCd == model.ProcessCd && lpo.MaterialCd == model.ProductNo && lpo.LotNo == model.LotNo
                                        select new { lpo.ReportId, lpo.MachineCd, lpo.WorkerCd, lpo.JobCd, lpo.LotWorkerNote, mpo.OperationName })
                                        .OrderByDescending(x => x.ReportId).ToListAsync();

                var listMan = new List<LotManModel>();
                for (int i = 0; i < queryList.Count; i++)
                {
                    listMan.Add(new LotManModel
                    {
                        ReportId = queryList[i].ReportId,
                        MachineCd = queryList[i].MachineCd,
                        JobCd = queryList[i].JobCd,
                        LotWorkerNote = queryList[i].LotWorkerNote,
                        WorkerCd = queryList[i].WorkerCd,
                        JobCdName = queryList[i].OperationName,
                        DivisionCd = model.DivisionCd,
                        ProcessCd = model.ProcessCd,
                        ProductNo = model.ProductNo,
                        LotNo = model.LotNo,
                    });
                }

                _logger.LogInformation($"[GetItems] Count: {listMan.Count}");

                Generation.GenerateResponse(ref response, listMan);

            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return response;
        }
    }
}
