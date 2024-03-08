using System.ComponentModel.DataAnnotations;

namespace SparkNest.Services.IdentityServiceAPI.DTOs
{
    public class SignUpDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
