using System.ComponentModel.DataAnnotations;

namespace MCSAndroidAPI.Models
{
    public class LotBaseModel
    {
        [Required]
        public string DivisionCd { get; set; } = null!;

        [Required]
        public string ProcessCd { get; set; } = null!;

        [Required]
        public string ProductNo { get; set; } = null!;

        [Required]
        public string LotNo { get; set; } = null!;
    }

    public class LotBaseFields
    {
        public const string DivisionCd = "DivisionCd";
        public const string ProcessCd = "ProcessCd";
        public const string ProductNo = "ProductNo";
        public const string LotNo = "LotNo";
    }

    public class LotBaseDisplay
    {
        public const string DivisionCd = "Division";
        public const string ProcessCd = "Process";
        public const string ProductNo = "Product no";
        public const string LotNo = "Lot no";
    }

    public class LotBaseMaxlength
    {
        public const int DivisionCd = 5;
        public const int ProcessCd = 10;
        public const int ProductNo = 30;
        public const int LotNo = 50;
    }
}
