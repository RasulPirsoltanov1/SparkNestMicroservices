using System.ComponentModel.DataAnnotations;

namespace SparkNest.UI.MVC.Models
{
    public class SignUpInput
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required,Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public string? Country { get; set; }
    }
}
