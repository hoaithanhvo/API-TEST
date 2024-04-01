using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;

namespace MCSAndroidAPI.Contracts
{
    public interface ILotScrapRepository : IRepositoryBase<TLotScrap>
    {
        Task<ResponseModel<List<LotScrapModel>>> GetItemsAsync(LotScrapModel model);
        Task<ResponseModel<object>> GetTypesAndReasonsAsync(DivisionCdAndProcessCdModel model, string productNo);
        Task<ResponseModel<List<string>>> GetListItemNoAsync(string productNo, string type);
        Task<ResponseModel<object>> CreateAsync(LotScrapModel model, string token);
        Task<ResponseModel<object>> UpdateAsync(LotScrapModel model, string token);
    }
}
