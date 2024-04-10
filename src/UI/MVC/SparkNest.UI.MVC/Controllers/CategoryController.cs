using Microsoft.AspNetCore.Mvc;

namespace SparkNest.UI.MVC.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
