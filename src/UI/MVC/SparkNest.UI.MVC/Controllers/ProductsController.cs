using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SparkNest.Common.Base.Services;
using SparkNest.UI.MVC.Models.Product;
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

        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAllProductsByUserIdAsync(_sharedIdentityService.UserId));
        }
        public async Task<IActionResult> Create()
        {
            var categories = await _productService.GetAllCateegoryAsync();
            ViewBag.CategoryList = categories;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM productCreateVM)
        {
            var categories = await _productService.GetAllCateegoryAsync();
            ViewBag.CategoryList = categories;
            if (!ModelState.IsValid)
                return View(productCreateVM);
            productCreateVM.UserId = _sharedIdentityService.UserId;
            var response = await _productService.CreateProductAsync(productCreateVM);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var categories = await _productService.GetAllCateegoryAsync();
            ViewBag.CategoryList = categories;
            var product = await _productService.GetByProductId(id);
            if (product == null)
                return BadRequest();
            return View(new ProductUpdateVM
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                Description = product.Description,
                Feature = product.Feature,
                Name = product.Name,
                Picture = product.Picture,
                Price = product.Price,
                UserId = product.UserId
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateVM productUpdateVM)
        {
            var categories = await _productService.GetAllCateegoryAsync();
            ViewBag.CategoryList = categories;
            productUpdateVM.UserId = _sharedIdentityService.UserId;
            if (!ModelState.IsValid)
            {
                return View(productUpdateVM);
            }
            var response = await _productService.UpdateProductAsync(productUpdateVM);
            if (response)
                return RedirectToAction(nameof(Index));
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
