using System.ComponentModel.DataAnnotations;

namespace MCSAndroidAPI.Models
{
    public class LotStopModel : LotBaseModel
    {
        public decimal? ReportId { get; set; }
        public DateTime? StopStrTime { get; set; }
        public DateTime? StopEndTime { get; set; }
        public decimal? StopHr { get; set; }
        public string? StopRsnCd { get; set; }
        public string? StopRsnName { get; set; }
        public string? StopNote { get; set; }
    }

    public class LotStopFields : LotBaseFields
    {
        public const string ReportId = "ReportId";
        public const string StopStrTime = "StopStrTime";
        public const string StopEndTime = "StopEndTime";
        public const string StopHr = "StopHr";
        public const string StopRsnCd = "StopRsnCd";
        public const string StopRsnName = "StopRsnName";
        public const string StopNote = "StopNote";
    }

    public class LotStopDisplay : LotBaseDisplay
    {
        public const string ReportId = "ReportId";
        public const string StopStrTime = "Start date";
        public const string StopEndTime = "End date";
        public const string StopHr = "Time";
        public const string StopRsnCd = "Reason";
        public const string StopNote = "Note";
    }

    public class LotStopMaxlength : LotBaseMaxlength
    {
        public const int StopHr = 4;
        public const int StopRsnCd = 5;
        public const int StopNote = 100;
    }
}
