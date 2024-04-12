using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Application.DTOs;

namespace SparkNest.UI.MVC.ViewComponents.UI
{
    public class ProductCommentPostVC : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string productId)
        {
            ViewBag.ProductId = productId;
            return View(new CreateCommentDTO());
        }
    }
}
