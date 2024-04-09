using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Models.Gallery;

namespace SparkNest.UI.MVC.ViewComponents.Admin
{
    [ViewComponent(Name = "AddPhotosToGallery")]
    public class AddPhotosToGalleryViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(string productId)
        {
            ViewBag.ProductId = productId;
            return View(new AddPhotosRequest());
        }

    }
}
