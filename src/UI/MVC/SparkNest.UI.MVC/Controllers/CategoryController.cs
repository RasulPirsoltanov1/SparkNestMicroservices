using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Controllers
{
    public class CategoryController : Controller
    {
        IProductService _productService;

        public CategoryController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> GetProductsByCategory(string Id)
        {
            var products = (await _productService.GetAllProductsAsync()).Where(x => x.CategoryId == Id||x.Category?.SubCategoryId==Id).ToList();
            return View(products);
        }
    }
}
