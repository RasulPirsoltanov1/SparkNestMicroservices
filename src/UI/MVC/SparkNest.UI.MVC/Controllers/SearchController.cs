using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Models.Product;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Controllers
{
    public class SearchController : Controller
    {
        IProductService _productService;

        public SearchController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Search(string key)
        {
            try
            {
                ViewBag.Key = key;
                var products = (await _productService.GetAllProductsAsync()).Where(x => x.Name.Contains(key) || x.Description.Contains(key) || x.Feature.Color.Contains(key)).ToList();
                return View(products);
            }
            catch (Exception ex)
            {
                return View(new List<ProductVM>());
            }
        }
    }
}
