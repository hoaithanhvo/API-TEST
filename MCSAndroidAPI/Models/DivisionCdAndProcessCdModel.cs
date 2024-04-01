using System.ComponentModel.DataAnnotations;

namespace MCSAndroidAPI.Models
{
    public class DivisionCdAndProcessCdModel
    {
        [Required]
        public string DivisionCd { get; set; } = null!;

        [Required]
        public string ProcessCd { get; set; } = null!;
    }
}
