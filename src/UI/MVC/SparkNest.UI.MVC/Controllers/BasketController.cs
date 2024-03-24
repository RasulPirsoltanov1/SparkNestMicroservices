using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Models.Baskets;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Controllers
{
    public class BasketController : Controller
    {
        IProductService _productService;
        IBasketService _basketService;

        public BasketController(IProductService productService, IBasketService basketService)
        {
            _productService = productService;
            _basketService = basketService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _basketService.Get());
        }
        public async Task<IActionResult> AddBasketItem(string productId, int quantiy = 1)
        {
            var product = await _productService.GetByProductId(productId);
            var basketItem = new BasketItemVM { ProductId = productId, Price = product.Price, ProductName = product.Name, Quantity = quantiy};
            await _basketService.AddBasketItem(basketItem);
            return RedirectToAction(nameof(Index));   
        }
        public async Task<IActionResult> RemoveBasketItem(string productId, int quantiy = 1)
        {
            await _basketService.RemoveBasketItem(productId);
            return RedirectToAction(nameof(Index));
        }
    }
}
