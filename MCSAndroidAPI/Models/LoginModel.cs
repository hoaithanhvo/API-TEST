using System.ComponentModel.DataAnnotations;

namespace MCSAndroidAPI.Models
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; } = null!;
    }
}
