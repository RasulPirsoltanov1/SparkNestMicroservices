using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Models;
using SparkNest.UI.MVC.Services.Interfaces;
using System.Diagnostics;

namespace SparkNest.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ServiceApiSettings _serviceApiSettings;
        IIdentityService _identityService;

        public HomeController(ILogger<HomeController> logger, ServiceApiSettings serviceApiSettings, IIdentityService identityService)
        {
            _logger = logger;
            _serviceApiSettings = serviceApiSettings;
            _identityService = identityService;
        }
        public async Task<IActionResult> IndexSet()
        {
         var resposne =   await _identityService.SignIn(new SignInInput
            {
                Email ="resulresull510@gmail.com",
                IsRememberMe = true,
                Password = "Rasul123."
            });
            return Ok(resposne);
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
