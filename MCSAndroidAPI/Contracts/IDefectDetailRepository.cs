using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;

namespace MCSAndroidAPI.Contracts
{
    public interface IDefectDetailRepository : IRepositoryBase<TDefectDetail>
    {
        Task<ResponseModel<object>> GetListAsync(string model, string lotNo);
        Task<ResponseModel<List<MaterialModel>>> GetMaterialsAsync(string model, string stageCd);
        ResponseModel<object> Create(DefectDetailModel model, string token);
    }
}
