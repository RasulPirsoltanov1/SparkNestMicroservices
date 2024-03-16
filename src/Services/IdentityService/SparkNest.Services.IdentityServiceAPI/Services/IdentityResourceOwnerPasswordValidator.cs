using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using SparkNest.Services.IdentityServiceAPI.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SparkNest.Services.IdentityServiceAPI.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existUser = await _userManager.FindByEmailAsync(context.UserName);
            if (existUser == null)
            {
                context.Result.CustomResponse = new Dictionary<string, object>
                {
                    {"errors",new List<string> {"username or password is invalid!"} }
                };
                return;
            }

            var result = await _userManager.CheckPasswordAsync(existUser, context.Password);
            if (!result)
            {
                context.Result.CustomResponse = new Dictionary<string, object>
                {
                    {"errors",new List<string> {"username or password is invalid!"} }
                };
                return; 
            }
              context.Result = new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password,new List<Claim>() { new Claim("role", "Admin") });
        }
    }
}
