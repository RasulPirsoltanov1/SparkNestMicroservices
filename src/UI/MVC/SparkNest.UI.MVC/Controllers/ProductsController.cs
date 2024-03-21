using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SparkNest.Common.Base.Services;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        IProductService _productService;
        ISharedIdentityService _sharedIdentityService;

        public ProductsController(IProductService productService, ISharedIdentityService sharedIdentityService)
        {
            _productService = productService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(await _productService.GetAllProductsByUserIdAsync(_sharedIdentityService.UserId));
        }
    }
}
