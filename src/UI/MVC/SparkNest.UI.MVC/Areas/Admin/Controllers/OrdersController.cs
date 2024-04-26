using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Models.Orders;
using SparkNest.UI.MVC.Models.Orders.StatusChange;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("[area]/[controller]/[action]")]
    public class OrdersController : Controller
    {
        IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            List<OrderVM> orders = await _orderService.GetAll();
            return View(orders);
        }
        [HttpPost]
        public async Task<bool> StatusChange(StatusChangeVM statusChangeVM)
        {
            var result = await _orderService.StatusChange(statusChangeVM);
            return result;
        }
    }
}
