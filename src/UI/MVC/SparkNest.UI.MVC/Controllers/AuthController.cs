using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Models;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Controllers
{
    public class AuthController : Controller
    {
        IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult SignIn()
        {
            return View();
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
                    result.Errors.ForEach(error => {
                        ModelState.AddModelError("", error);
                    });
                    return View(signInInput);
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
            return View();
        }
    }
}
