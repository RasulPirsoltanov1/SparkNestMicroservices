using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Application.DTOs;

namespace SparkNest.UI.MVC.ViewComponents.UI
{
    public class UploadProfileImageVC:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(new UploadProfileImageDTO());
        }
    }
}
