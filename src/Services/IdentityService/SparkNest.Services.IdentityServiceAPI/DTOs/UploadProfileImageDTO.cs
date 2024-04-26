using System.ComponentModel.DataAnnotations;

namespace SparkNest.Services.IdentityServiceAPI.DTOs
{
    public class UploadProfileImageDTO
    {
        [Required]
        public string UserNameOrEmail { get; set; }
        public string ImageUrl { get; set; }
    }
}
