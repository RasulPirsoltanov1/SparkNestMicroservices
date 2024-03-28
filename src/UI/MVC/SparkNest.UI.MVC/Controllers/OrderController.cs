using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Models.Orders;
using SparkNest.UI.MVC.Services.Concretes;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Controllers
{
    public class OrderController : Controller
    {
        IBasketService _basketService;
        IOrderService _orderService;

        public OrderController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.Get();
            ViewBag.basket = basket;
            return View(new CheckoutInfoVM());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoVM checkoutInfoVM)
        {
            //var response = await _orderService.CreateOrder(checkoutInfoVM);
            
            
            //asyncron
            var suspendStatus = await _orderService.SuspendOrder(checkoutInfoVM);
            if (!suspendStatus.IsSuccessfull)
            {
                var basket = await _basketService.Get();
                ViewBag.basket = basket;
                TempData["error"] = suspendStatus.Error;
                return View();
            }
            //return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = response.OrderId });
            return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = new Random().Next(1,1000)});
        }
        public IActionResult SuccessfulCheckout(int orderId)
        {
            ViewBag.orderId = orderId;
            return View();
        }
    }
}
