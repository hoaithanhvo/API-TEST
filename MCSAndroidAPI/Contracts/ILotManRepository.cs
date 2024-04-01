using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;

namespace MCSAndroidAPI.Contracts
{
    public interface ILotManRepository : IRepositoryBase<TLotWorker>
    {
        Task<ResponseModel<List<LotManModel>>> GetItemsAsync(LotManModel model);
        Task<ResponseModel<List<ProcessOpeModel>>> GetJobsAsync(DivisionCdAndProcessCdModel model);

        Task<ResponseModel<object>> CreateAsync(LotManModel model, string token);

        Task<ResponseModel<object>> UpdateAsync(LotManModel model, string token);
    }
}
