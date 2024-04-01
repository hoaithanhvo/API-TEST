using System.ComponentModel.DataAnnotations;

namespace MCSAndroidAPI.Models
{
    public class ProductionResultSearchModel
    {
        [Required]
        public string DivisionCd { get; set; } = null!;
        [Required]
        public string ProcessCd { get; set; } = null!;
        [Required]
        public string RoutingCd { get; set; } = null!;
        public DateTime? ProdDateFrom { get; set; }
        public DateTime? ProdDateTo { get; set; }
        public string? ProductNo { get; set; }
        public string? LotNo { get; set; }

        public string? ShiftCd { get; set; }

        public string? LineCd { get; set; }
    }
}
