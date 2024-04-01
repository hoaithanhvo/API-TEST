using AutoMapper;
using Azure;
using MCSAndroidAPI.Constants;
using MCSAndroidAPI.Contracts;
using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;
using MCSAndroidAPI.Utility;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MCSAndroidAPI.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private readonly NidecMCSContext _nidecMCSContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public CommonRepository(NidecMCSContext nidecMCSContext, IMapper mapper, ILogger logger)
        {
            _nidecMCSContext = nidecMCSContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResponseModel<List<DivisionModel>>> GetDivisionsAsync()
        {
            var response = new ResponseModel<List<DivisionModel>>();
            try
            {
                var models = await _nidecMCSContext.MDivisions.Select(s => _mapper.Map<DivisionModel>(s)).ToListAsync();
                
                _logger.LogInformation($"[GetDivisions] Count: {models.Count}");

                Generation.GenerateResponse(ref response, models);

            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return response;
        }

        public async Task<ResponseModel<List<LineModel>>> GetLinesAsync(DivisionCdAndProcessCdModel model)
        {
            var response = new ResponseModel<List<LineModel>>();

            try
            {
                var models = await _nidecMCSContext.MLines.Where(x => x.DivisionCd == model.DivisionCd && x.ProcessCd == model.ProcessCd)
                    .Select(l => _mapper.Map<LineModel>(l)).ToListAsync();

                _logger.LogInformation($"[GetLines] Count: {models.Count}");

                Generation.GenerateResponse(ref response, models);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return response;
        }

        public async Task<ResponseModel<List<RoutingModel>>> GetRoutingsAsync(DivisionCdAndProcessCdModel model)
        {
            var response = new ResponseModel<List<RoutingModel>>();
            try
            {
                var models = await _nidecMCSContext.MRoutings.Where(x => x.DivisionCd == model.DivisionCd && x.ProcessCd == model.ProcessCd)
                    .Select(r => _mapper.Map<RoutingModel>(r)).ToListAsync();

                _logger.LogInformation($"[GetRoutings] Count: {models.Count}");

                Generation.GenerateResponse(ref response, models);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return response;
        }

        public async Task<ResponseModel<List<ProcessModel>>> GetProcessesAsync(string divisionCd)
        {
            var response = new ResponseModel<List<ProcessModel>>();
            try
            {
                var models = await _nidecMCSContext.MProcesses.Where(x => x.DivisionCd == divisionCd)
                    .Select(p => _mapper.Map<ProcessModel>(p) ).ToListAsync();

                _logger.LogInformation($"[GetProcesses] Count: {models.Count}");
                Generation.GenerateResponse(ref response, models);

            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return response;
        }

        public async Task<ResponseModel<List<ShiftModel>>> GetShiftsAsync(DivisionCdAndProcessCdModel model)
        {
            var response = new ResponseModel<List<ShiftModel>>();
            try
            {
                var models = await _nidecMCSContext.MShifts.Where(x => x.DivisionCd == model.DivisionCd && x.ProcessCd == model.ProcessCd)
                    .Select(s => _mapper.Map<ShiftModel>(s)).ToListAsync();

                _logger.LogInformation($"[GetShifts] Count: {models.Count}");

                Generation.GenerateResponse(ref response, models);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return response;
        }

        public async Task<ResponseModel<List<ProcessOpeModel>>> GetJobsAsync(DivisionCdAndProcessCdModel model)
        {
            var response = new ResponseModel<List<ProcessOpeModel>>();
            try
            {
                var models = await _nidecMCSContext.MProcessOpes.Where(x => x.DivisionCd == model.DivisionCd && x.ProcessCd == model.ProcessCd)
                    .Select(p => _mapper.Map<ProcessOpeModel>(p)).ToListAsync();

                _logger.LogInformation($"[GetJobs] Count: {models.Count}");

                Generation.GenerateResponse(ref response, models);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return response;
        }

        public async Task<ResponseModel<List<WorkerModel>>> GetLeadersAsync(string divisionCd)
        {
            var response = new ResponseModel<List<WorkerModel>>();

            try
            {
                var models = await _nidecMCSContext.MWorkers.Where(x => x.DivisionCd ==  divisionCd && 
                    (x.RankCd == SystemConstants.RankCode.RANK_CD_20 || x.RankCd == SystemConstants.RankCode.RANK_CD_30 || x.RankCd == null))
                    .Select(w => _mapper.Map<WorkerModel>(w)).ToListAsync();

                _logger.LogInformation($"[GetLeaders] Count: {models.Count}");

                Generation.GenerateResponse(ref response, models);
            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return response;
        }

        public async Task SetTriggerAsync(LotBaseModel model, string? username)
        {
            try
            {
                var product = await _nidecMCSContext.TLotProducts.Where(x => x.DivisionCd == model.DivisionCd && x.ProcessCd == model.ProcessCd &&
                        x.ProductNo == model.ProductNo && x.LotNo == model.LotNo).FirstOrDefaultAsync();

                if (product != null)
                {
                    product.UpdatedDate = DateTime.Now;
                    product.UpdatedBy = username;

                    _logger.LogInformation($"[SetTrigger] {SystemConstants.Status.SUCCESS}");
                } else
                {
                    _logger.LogWarning($"[SetTrigger] {SystemConstants.Message.PRODUCT_NOT_FOUND}");
                }


            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }

        public async Task<ResponseModel<List<DefectReasonModel>>> GetDefectReasonsAsync(string divisionCd, string processCd)
        {
            var response = new ResponseModel<List<DefectReasonModel>>();

            try
            {
                var models = await _nidecMCSContext.MDefectReasons.Where(x => x.DivisionCd == divisionCd && x.ProcessCd == processCd)
                    .OrderBy(x => x.DefectRsnCd)
                    .Select(d => _mapper.Map<DefectReasonModel>(d)).ToListAsync();

                _logger.LogInformation($"[GetDefectReasons] Count: {models.Count}");

                Generation.GenerateResponse(ref response, models);

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
