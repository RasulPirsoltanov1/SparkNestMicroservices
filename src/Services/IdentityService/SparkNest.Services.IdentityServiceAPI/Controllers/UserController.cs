using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SparkNest.Common.DTOs;
using SparkNest.Services.IdentityServiceAPI.DTOs;
using SparkNest.Services.IdentityServiceAPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SparkNest.Services.IdentityServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUpAsync(SignUpDTO signUpDTO)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = signUpDTO.UserName,
                Email = signUpDTO.Email,
                Country = signUpDTO.Country,
            };
            var result = await _userManager.CreateAsync(applicationUser, signUpDTO.Password);
            if (!result.Succeeded)
            {
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(x=>x.Description).ToList(),400));
            }
            return NoContent();
        }
    }
}
