using SparkNest.UI.MVC.Models.Discount;

namespace SparkNest.UI.MVC.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountVM> GetDiscount(string discountCode);
    }
}
