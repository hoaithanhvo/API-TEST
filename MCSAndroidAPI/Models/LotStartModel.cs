using System.ComponentModel.DataAnnotations;

namespace MCSAndroidAPI.Models
{
    public class LotStartModel : LotBaseModel
    {
        public DateTime? ProductStrDt { get; set; }

        public DateTime? ProductEndDt { get; set; }
        public decimal? WorkerNum { get; set; }
        public string? LeaderWorkerNo { get; set; }
        public decimal? RestHr { get; set; }
    }

    public class LotStartFields : LotBaseFields
    {
        public const string ProductStrDt = "ProductStrDt";
        public const string ProductEndDt = "ProductEndDt";
        public const string WorkerNum = "WorkerNum";
        public const string LeaderWorkerNo = "LeaderWorkerNo";
        public const string RestHr = "RestHr";
    }

    public class LotStartDisplay : LotBaseDisplay
    {
        public const string ProductStrDt = "Start date";
        public const string ProductEndDt = "End date";
        public const string WorkerNum = "Worker";
        public const string LeaderWorkerNo = "Leader";
        public const string RestHr = "Rest time";
    }

    public class LotStartMaxlength : LotBaseMaxlength
    {
        public const int WorkerNum = 7;
        public const int LeaderWorkerNo = 10;
        public const int RestHr = 5;
    }
}
