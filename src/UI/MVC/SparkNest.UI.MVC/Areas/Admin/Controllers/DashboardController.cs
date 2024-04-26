using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("[area]/[controller]/[action]")]
    public class DashboardController : Controller
    {
        IOrderService _orderService;
        IProductService _productService;

        public DashboardController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = (await _orderService.GetAll());
            decimal sales = 0;
            foreach (var order in orders)
            {
                foreach (var oItem in order.OrderItems)
                {
                    sales += oItem.Price;
                }
            }
            ViewBag.OrdersCount = orders.Count;
            ViewBag.Delivered = orders.Where(x=>x.Status==1).Count();
            ViewBag.Arrived = orders.Where(x=>x.Status==2).Count();
            ViewBag.Sales = sales;
            ViewBag.Products = (await _productService.GetAllProductsAsync()).OrderByDescending(x=>x.CreatedDate).Take(4).ToList();
            return View();
        }
    }
}
