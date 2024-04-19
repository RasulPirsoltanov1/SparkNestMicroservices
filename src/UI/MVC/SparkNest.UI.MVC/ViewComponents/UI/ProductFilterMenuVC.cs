using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Models.Products;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.ViewComponents.UI
{
    public class ProductFilterMenuVC : ViewComponent
    {
        IProductService _productService;

        public ProductFilterMenuVC(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.Categories = await _productService.GetAllCateegoryAsync();
            return View(new ProductsFilterVM());
        }
    }
}
