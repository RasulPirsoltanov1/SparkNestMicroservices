using SparkNest.UI.MVC.Models.Orders;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Services.Concretes
{
    public class OrderService : IOrderService
    {
        public Task<OrderStatusVM> CreateOrder(CheckoutInfoVM checkoutInfoVM)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderVM>> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task SuspendOrder(CheckoutInfoVM checkoutInfoVM)
        {
            throw new NotImplementedException();
        }
    }
}
