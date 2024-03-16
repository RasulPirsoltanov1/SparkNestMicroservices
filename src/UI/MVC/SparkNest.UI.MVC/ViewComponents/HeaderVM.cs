using Microsoft.AspNetCore.Mvc;

namespace SparkNest.UI.MVC.ViewComponents
{
    public class HeaderVM:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
