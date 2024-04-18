using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Application.DTOs.Produtcs;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Controllers
{
    public class RatingController : Controller
    {
        IProductService _productService;

        public RatingController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> RateProduct(ProductRateDTO productRateDTO)
        {
            await _productService.RateAsync(productRateDTO);
            return RedirectToAction("Detail", "Home", new
            {
                Id = productRateDTO.ProductId
            });
        }
    }
}
