using System.ComponentModel.DataAnnotations;

namespace MCSAndroidAPI.Models
{
    public class LotDefectModel : LotBaseModel
    {
        public decimal? ReportId { get; set; }
        public DateTime? ReportTime { get; set; }
        public decimal? DefectQty { get; set; }
        public string? DefectRsnCd { get; set; }
        public string? DefectRsnName { get; set; }
        public string? DefectNote { get; set; }
    }

    public class LotDefectFields : LotBaseFields
    {
        public const string ReportId = "ReportId";
        public const string ReportTime = "ReportTime";
        public const string DefectQty = "DefectQty";
        public const string DefectRsnCd = "DefectRsnCd";
        public const string DefectRsnName = "DefectRsnName";
        public const string DefectNote = "DefectNote";
    }

    public class LotDefectDisplay : LotBaseDisplay
    {
        public const string ReportId = "Report";
        public const string ReportTime = "Time";
        public const string DefectQty = "Qty";
        public const string DefectRsnCd = "Reason";
        public const string DefectNote = "Note";
    }

    public class LotDefectMaxlength : LotBaseMaxlength
    {
        public const int DefectQty = 7;
        public const int DefectRsnCd = 10;
        public const int DefectNote = 100;
    }
}
