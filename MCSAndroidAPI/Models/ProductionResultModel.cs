namespace MCSAndroidAPI.Models
{
    public class ProductionResultModel
    {
        public string? DivisionCd { get; set; }

        public string? ShiftCd { get; set; }

        public string? ShiftName { get; set; }

        public string? LineCd { get; set; }

        public string? LineName { get; set; }

        public string? ProductNo { get; set; }

        public string? ProductName { get; set; }

        public string? LotNo { get; set; }

        public string? BoxNo { get; set; }

        public string? Status { get; set; }

        public decimal PlanQty { get; set; }

        public decimal Finish { get; set; }

        public string? ProductDay { get; set; }

        public string? ProcessCd { get; set; }

        public string? ProcessName { get; set; }

        public string? MoNo { get; set; }

        public DateTime? ProdStartTime { get; set; }

        public DateTime? ProdEndTime { get; set; }

        public decimal Time { get; set; }

        public string? CreatedBy { get; set; }

        public string? LeaderNo { get; set; }
        public decimal WorkerNum { get; set; }

        public decimal TotalTime { get; set; }

        public decimal RestTime { get; set; }

        public decimal WorkingTime { get; set; }

        public decimal ManHour { get; set; }

        public decimal ProdQty { get; set; }

        public decimal Sap { get; set; }

        public string? SapLinkStatus { get; set; }

        public decimal ProdWorkunit { get; set; }

        public decimal WorkunitNum { get; set; }

        public decimal ProdTact { get; set; }

        public decimal DefectQty { get; set; }

        public decimal StopHr { get; set; }

        public decimal OnlineRate { get; set; }
        public decimal GoodQty { get; set; }

        public string? Type { get; set; }
        public string? LeaderName { get; set; }
    }
}
