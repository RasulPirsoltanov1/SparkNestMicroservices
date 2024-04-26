using Microsoft.AspNetCore.Mvc;

namespace SparkNest.UI.MVC.ViewComponents.UI
{
    public class HeaderSearchbarVC:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
