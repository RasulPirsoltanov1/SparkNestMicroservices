using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Models;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Controllers
{
    public class AuthController : Controller
    {
        IIdentityService _identityService;
        IHttpContextAccessor _contextAccessor;

        public AuthController(IIdentityService identityService, IHttpContextAccessor contextAccessor)
        {
            _identityService = identityService;
            _contextAccessor = contextAccessor;
        }

        public IActionResult SignIn()
        {
            return View();
        }
        public async Task<IActionResult> SignOut()
        {
            await _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _identityService.RevokeRefreshToken();
            return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInInput signInInput)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(signInInput);
                }
                var result = await _identityService.SignIn(signInInput);
                if (result.Errors != null)
                {
                    result.Errors.ForEach(error =>
                    {
                        ModelState.AddModelError("", error);
                    });
                    return View(signInInput);
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
            return RedirectToAction("Index", "User");
        }
    }
}
