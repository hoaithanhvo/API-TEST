using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;

namespace MCSAndroidAPI.Contracts
{
    public interface ICommonRepository
    {
        Task<ResponseModel<List<DivisionModel>>>  GetDivisionsAsync();
        Task<ResponseModel<List<ProcessModel>>> GetProcessesAsync(string divisionCd);
        Task<ResponseModel<List<ShiftModel>>>  GetShiftsAsync(DivisionCdAndProcessCdModel model);
        Task<ResponseModel<List<LineModel>>> GetLinesAsync(DivisionCdAndProcessCdModel model);
        Task<ResponseModel<List<RoutingModel>>> GetRoutingsAsync(DivisionCdAndProcessCdModel model);
        Task<ResponseModel<List<ProcessOpeModel>>> GetJobsAsync(DivisionCdAndProcessCdModel model);
        Task<ResponseModel<List<DefectReasonModel>>> GetDefectReasonsAsync(string divisionCd, string processCd);
        Task<ResponseModel<List<WorkerModel>>> GetLeadersAsync(string divisionCd);
        Task SetTriggerAsync(LotBaseModel model, string? username);
    }
}
