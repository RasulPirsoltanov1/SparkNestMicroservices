using Microsoft.AspNetCore.Mvc;

namespace SparkNest.UI.MVC.Controllers
{
    public class HomePageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
