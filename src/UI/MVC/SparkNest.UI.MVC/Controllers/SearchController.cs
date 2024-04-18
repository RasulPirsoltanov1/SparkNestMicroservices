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
                var products = (await _productService.GetAllProductsAsync()).Where(x => x.Name.ToLower().Contains(key.ToLower()) || x.Description.Contains(key) || x.Feature.Color.Contains(key)).ToList();
                return View(products);
            }
            catch (Exception ex)
            {
                return View(new List<ProductVM>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> SearchResultForAjax(string key)
        {
            try
            {
                if (key == null)
                {
                    return Ok(new List<ProductVM>());
                }
                ViewBag.Key = key;
                List<ProductVM>? products = (await _productService.GetAllProductsAsync()).Where(x => x.Name.ToLower().Contains(key.ToLower())).ToList();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return Ok(new List<ProductVM>());
            }
        }

        [HttpGet("[controller]/[action]")]
        public async Task<IActionResult> SearchFromAjax([FromQuery]string key)
        {
            try
            {
                ViewBag.Key = key;
                var products = (await _productService.GetAllProductsAsync()).Where(x => x.Name.ToLower().Contains(key.ToLower()) || x.Description.Contains(key) || x.Feature.Color.Contains(key)).ToList();
                return View(products);
            }
            catch (Exception ex)
            {
                return View(new List<ProductVM>());
            }
        }
    }
}
