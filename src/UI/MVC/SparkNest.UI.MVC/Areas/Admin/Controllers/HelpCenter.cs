using Microsoft.AspNetCore.Mvc;

namespace SparkNest.UI.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HelpCenter : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Answer(string clientConnectionId, string message, string userName)
        {
            ViewBag.UserName = userName;
            ViewBag.ClientConnectionId = clientConnectionId;
            ViewBag.Message = message;
            return View();
        }
    }
}
