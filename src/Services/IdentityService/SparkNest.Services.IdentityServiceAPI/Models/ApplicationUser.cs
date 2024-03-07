using Microsoft.AspNetCore.Identity;

namespace SparkNest.Services.IdentityServiceAPI.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string? Country { get; set; }
        public string? ImageUrl{ get; set; }
    }
}
