using SparkNest.UI.MVC.Models.Orders;

namespace SparkNest.UI.MVC.Services.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Syncron comunication
        /// </summary>
        /// <param name="checkoutInfoVM"></param>
        /// <returns></returns>
        Task<OrderStatusVM> CreateOrder(CheckoutInfoVM checkoutInfoVM);
        /// <summary>
        /// Asyncron comunication
        /// </summary>
        /// <param name="checkoutInfoVM"></param>
        /// <returns></returns>
        Task<OrderSuspendStatusVM> SuspendOrder(CheckoutInfoVM checkoutInfoVM);

        Task<List<OrderVM>> GetAllOrders(); 
    }
}
