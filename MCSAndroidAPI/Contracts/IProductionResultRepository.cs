using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;

namespace MCSAndroidAPI.Contracts
{
    public interface IProductionResultRepository
    {
        Task<ResponseModel<List<DivisionModel>>> GetDivisionsAsync();
        Task<ResponseModel<List<ProcessModel>>> GetProcessesAsync(string divisionCd);

        Task<ResponseModel<object>> GetRoutingsShiftsLinesAsync(DivisionCdAndProcessCdModel model);

        Task<ResponseModel<List<ProductionResultModel>>> GetItemsAsync(ProductionResultSearchModel model);
    }
}
