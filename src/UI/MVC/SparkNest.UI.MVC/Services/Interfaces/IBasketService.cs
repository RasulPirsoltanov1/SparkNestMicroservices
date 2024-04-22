using SparkNest.UI.MVC.Controllers;
using SparkNest.UI.MVC.Models.Baskets;

namespace SparkNest.UI.MVC.Services.Interfaces
{
    public interface IBasketService
    {
        Task<bool> SaveOrUpdate(BasketVM basketVM);
        Task<BasketVM> Get();
        Task<bool> Delete();
        Task AddBasketItem(BasketItemVM basketItemVM);    
        Task<bool> RemoveBasketItem(string productId);
        //Task<bool> ApplyDicount(string discountCode);
        //Task<bool> CancelApplyDicount();
        Task<bool> TestSend(TestBasket testBasket);
    }
}
