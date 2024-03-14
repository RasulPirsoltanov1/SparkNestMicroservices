using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Models;
using System.Diagnostics;

namespace SparkNest.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ServiceApiSettings _serviceApiSettings;

        public HomeController(ILogger<HomeController> logger, ServiceApiSettings serviceApiSettings)
        {
            _logger = logger;
            _serviceApiSettings = serviceApiSettings;
        }
        public IActionResult IndexSet()
        {

            return Ok(_serviceApiSettings);
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
