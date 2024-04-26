using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.ViewComponents.UI
{
    public class HeaderNavbarVC: ViewComponent
    {
        IProductService _productService;

        public HeaderNavbarVC(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _productService.GetAllCateegoryAsync());
        }
    }
}
