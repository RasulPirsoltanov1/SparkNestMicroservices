using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SparkNest.UI.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin,Moderator")]
    [Route("[area]/[controller]/[action]")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
