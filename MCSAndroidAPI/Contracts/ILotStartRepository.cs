using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;

namespace MCSAndroidAPI.Contracts
{
    public interface ILotStartRepository : IRepositoryBase<TLotProduct>
    {
        Task<ResponseModel<object>> GetItemAsync(LotStartModel model);
        Task<ResponseModel<object>> UpdateAsync(LotStartModel model, string token);
    }
}
