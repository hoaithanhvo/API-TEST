using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;

namespace MCSAndroidAPI.Contracts
{
    public interface ILotStopRepository : IRepositoryBase<TLotStop>
    {
        Task<ResponseModel<List<LotStopModel>>> GetItemsAsync(LotStopModel model);
        Task<ResponseModel<List<StopReasonModel>>> GetReasonsAsync(DivisionCdAndProcessCdModel model);
        Task<ResponseModel<object>> CreateAsync(LotStopModel model, string token);
        Task<ResponseModel<object>> UpdateAsync(LotStopModel model, string token);
        Task<ResponseModel<object>> DeleteAsync(LotStopModel model, string token);
    }
}
