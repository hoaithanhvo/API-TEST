using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;

namespace MCSAndroidAPI.Contracts
{
    public interface ILotDefectRepository : IRepositoryBase<TLotDefect>
    {
        Task<ResponseModel<List<LotDefectModel>>> GetItemsAsync(LotDefectModel model);
        Task<ResponseModel<List<DefectReasonModel>>> GetReasonsAsync(DivisionCdAndProcessCdModel model);
        Task<ResponseModel<object>> CreateAsync(LotDefectModel model, string token);
        Task<ResponseModel<object>> UpdateAsync(LotDefectModel model, string token);
        Task<ResponseModel<object>> DeleteAsync(LotDefectModel model, string token);
    }
}
