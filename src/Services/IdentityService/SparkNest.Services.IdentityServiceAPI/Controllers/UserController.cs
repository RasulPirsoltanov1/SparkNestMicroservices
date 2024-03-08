using IdentityServer4.Hosting.LocalApiAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SparkNest.Common.DTOs;
using SparkNest.Services.IdentityServiceAPI.DTOs;
using SparkNest.Services.IdentityServiceAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace SparkNest.Services.IdentityServiceAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    public class UserController : ControllerBase
    {
        UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Okey");
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
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
            }
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if (user == null)
                return BadRequest();
            return Ok(new
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Countyr = user.Country,
            });
        }
    }
}
