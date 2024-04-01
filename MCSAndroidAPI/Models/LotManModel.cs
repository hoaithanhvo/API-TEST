using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MCSAndroidAPI.Models
{
    public class LotManModel : LotBaseModel
    {
        public decimal? ReportId { get; set; }
        public string? MachineCd { get; set; }
        public string? WorkerCd { get; set; }
        public string? JobCd { get; set; }
        public string? JobCdName { get; set; }
        public string? LotWorkerNote { get; set; }
    }

    public class LotManFields : LotBaseFields
    {
        public const string ReportId = "ReportId";
        public const string JobCd = "JobCd";
        public const string JobCdName = "JobCdName";
        public const string MachineCd = "MachineCd";
        public const string WorkerCd = "WorkerCd";
        public const string LotWorkerNote = "LotWorkerNote";
    }

    public class LotManDisplay : LotBaseDisplay
    {
        public const string ReportId = "Report";
        public const string JobCd = "Job";
        public const string JobCdName = "Job name";
        public const string MachineCd = "Machine";
        public const string WorkerCd = "Worker";
        public const string LotWorkerNote = "Note";
    }

    public class LotManMaxlength : LotBaseMaxlength
    {
        public const int JobCd = 5;
        public const int MachineCd = 5;
        public const int WorkerCd = 10;
        public const int LotWorkerNote = 100;
    }
}
