namespace MCSAndroidAPI.Contracts
{
    public interface IRepositoryWrapper
    {
        IAuthenticateRepository Authenticate { get; }
        IProductionResultRepository ProductionResult { get; }
        ILotManRepository LotMan { get; }
        ILotDefectRepository LotDefect { get; }

        ILotScrapRepository LotScrap { get; }
        ILotStopRepository LotStop { get; }
        ILotStartRepository LotStart { get; }
        IDefectDetailRepository DefectDetail { get; }

        Task SaveAsync();
    }
}
