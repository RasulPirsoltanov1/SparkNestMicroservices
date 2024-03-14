using System.ComponentModel.DataAnnotations;

namespace SparkNest.UI.MVC.Models
{
    public class SignInInput
    {
        [Display(Name = "Email addresiniz.")]
        public string Email { get; set; }
        [Display(Name = "Sifrəniz.")]
        public string Password { get; set; }
        [Display(Name = "Bu cihazda xatirlansin?")]
        public bool IsRememberMe { get; set; }
    }
}
