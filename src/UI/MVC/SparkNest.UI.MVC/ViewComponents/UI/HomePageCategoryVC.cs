using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.ViewComponents.UI
{
    public class HomePageCategoryVC:ViewComponent
    {
        IProductService _productService;

        public HomePageCategoryVC(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _productService.GetAllCateegoryAsync();
            return View(categories.Take(6).ToList());
        }
    }
}
