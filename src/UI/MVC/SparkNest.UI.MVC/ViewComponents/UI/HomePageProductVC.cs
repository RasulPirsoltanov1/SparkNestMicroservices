using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.ViewComponents.UI
{
    public class HomePageProductVC:ViewComponent
    {
        IProductService _productService;

        public HomePageProductVC(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View((await _productService.GetAllProductsAsync()).Take(6).ToList());
        }
    }
}
