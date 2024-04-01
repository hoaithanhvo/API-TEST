using AutoMapper;
using MCSAndroidAPI.Constants;
using MCSAndroidAPI.Contracts;
using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;
using MCSAndroidAPI.Utility;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MCSAndroidAPI.Repositories
{
    public class LotStartRepository : RepositoryBase<TLotProduct>, ILotStartRepository
    {

        private ICommonRepository _commonRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public LotStartRepository(NidecMCSContext nidecMCSContext, IMapper mapper, ILogger logger) : base(nidecMCSContext, mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _commonRepository = new CommonRepository(nidecMCSContext, mapper, _logger);
        }

        public async Task<ResponseModel<object>> GetItemAsync(LotStartModel model)
        {
            var response = new ResponseModel<object>();

            try
            {
                var leaders = await _commonRepository.GetLeadersAsync(model.DivisionCd);

                if (leaders.status == SystemConstants.Status.ERROR)
                {
                    response.message = leaders.message;
                }

                var item = await this.FindByCondition(x => x.DivisionCd == model.DivisionCd && x.ProcessCd == model.ProcessCd &&
                    x.ProductNo == model.ProductNo && x.LotNo == model.LotNo).FirstOrDefaultAsync();

                var lotStart = _mapper.Map<LotStartModel>(item);

                var data = new {lotStart, leaders = leaders.data };

                _logger.LogInformation($"[GetItem]\nproductNo: {data.lotStart.ProductNo}\nleaders: count = {data.leaders?.Count}");

                Generation.GenerateResponse(ref response, data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return response;
        }

        public async Task<ResponseModel<object>> UpdateAsync(LotStartModel model, string token)
        {
            var response = new ResponseModel<object>();

            try
            {
                // get the username using the access token
                var principal = Validation.ValidateToken(token);
                var username = principal?.Identity?.Name;

                var item = await this.FindByCondition(x => x.DivisionCd == model.DivisionCd && x.ProcessCd == model.ProcessCd &&
                    x.ProductNo == model.ProductNo && x.LotNo == model.LotNo).FirstOrDefaultAsync();

                if (item == null)
                {
                    _logger.LogWarning($"[Update] {SystemConstants.Message.NOT_FOUND}");
                    Generation.GenerateResponse(ref response, null, false, SystemConstants.Message.NOT_FOUND);
                }
                else
                {
                    item.ProductStrDt = model.ProductStrDt;
                    item.ProductEndDt = model.ProductEndDt;
                    item.WorkerNum = model.WorkerNum;
                    item.LeaderWorkerNo = model.LeaderWorkerNo;
                    item.RestHr = model.RestHr;
                    item.LotStatusCode = 2;
                    item.UpdatedDate = DateTime.Now;
                    item.UpdatedBy = username;

                    this.Update(item);

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
