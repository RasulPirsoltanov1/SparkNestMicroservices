using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Application.DTOs.Produtcs;

namespace SparkNest.UI.MVC.ViewComponents.UI
{
    public class ProductRatePostVC:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string productId)
        {
            ViewBag.ProductId = productId;
            return View(new ProductRateDTO());
        }
    }
}
