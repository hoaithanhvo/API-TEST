using System.ComponentModel.DataAnnotations;

namespace MCSAndroidAPI.Models
{
    public class DefectDetailModel
    {
        public string? Model { get; set; }
        public string? LotNo { get; set; }
        public string? WorkerCd { get; set; }
        public decimal? NgQty { get; set; }
        public string? StageCd { get; set; }
        public string? DefectCd { get; set; }
        public string? Materials { get; set; }
    }

    public class DefectDetailDisplay
    {
        public const string Model = "Model";
        public const string LotNo = "Lot no";
        public const string WorkerCd = "Worker";
        public const string NgQty = "NG";
        public const string StageCd = "Stage";
        public const string DefectCd = "Reason";
        public const string Materials = "Materials";
    }

    public class DefectDetailMaxlength
    {
        // WorkerCd length is nvarchar(MAX)
        public const int Model = 30;
        public const int LotNo = 30;
        public const int NgQty = 10;
        public const int StageCd = 20;
        public const int DefectCd = 10;
    }
}
