using SparkNest.Common.Base.Services;
using SparkNest.Common.DTOs;
using SparkNest.UI.MVC.Controllers;
using SparkNest.UI.MVC.Models.Baskets;
using SparkNest.UI.MVC.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace SparkNest.UI.MVC.Services.Concretes
{
    public class BasketService : IBasketService
    {
        HttpClient _httpClient;
        IDiscountService _discountService;

        public BasketService(HttpClient httpClient, IDiscountService discountService)
        {
            _httpClient = httpClient;
            _discountService = discountService;
        }

        public async Task<bool> SaveOrUpdate(BasketVM basketVM)
        {
            var jsonContent = JsonSerializer.Serialize(basketVM);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsJsonAsync("baskets", basketVM);

            return response.IsSuccessStatusCode;
        }
        public async Task<BasketVM> Get()
        {
            var response = await _httpClient.GetAsync("baskets");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<Response<BasketVM>>()).Data;
            }
            return null;
        }

        public async Task<bool> RemoveBasketItem(string productId)
        {
            var basket = await Get();

            if (basket == null)

            {
                return false;
            }

            var deleteBasketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == productId);

            if (deleteBasketItem == null)
            {
                return false;
            }

            var deleteResult = basket.BasketItems.Remove(deleteBasketItem);

            if (!deleteResult)
            {
                return false;
            }

            if (!basket.BasketItems.Any())
            {
                basket.DiscountCode = null;
            }

            return await SaveOrUpdate(basket);
        }

        public async Task<bool> Delete()
        {
            await _httpClient.DeleteAsync("baskets");
            return true;
        }
        public async Task AddBasketItem(BasketItemVM basketItemVM)
        {
            var basket = await Get();
            if (basket != null)
            {
                var existingBasketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == basketItemVM.ProductId);
                if (existingBasketItem != null)
                {
                    existingBasketItem.Quantity += 1;
                }
                else
                {
                    basket.BasketItems.Add(basketItemVM);
                }
            }
            else
            {
                basket = new BasketVM();
                basket.BasketItems.Add(basketItemVM);
            }

            await SaveOrUpdate(basket);
        }

        public async Task<bool> ApplyDicount(string discountCode)
        {
            await CancelApplyDicount();
            var basket =await Get();
            if (basket == null)
                return false;
            var hasDiscount = await _discountService.GetDiscount(discountCode);
            if(hasDiscount == null)
            {
                return false;
            }
            basket.DiscountRate = hasDiscount.Rate;
            basket.DiscountCode = hasDiscount.Code;
            await SaveOrUpdate(basket);
            return true;

        }

        public async Task<bool> CancelApplyDicount()
        {
            BasketVM basket = await Get();
            if (basket == null || basket.DiscountCode == null)
                return false;
            basket.CancelDiscount();
            await SaveOrUpdate(basket);
            return true;
        }

        public async Task<bool> TestSend(TestBasket testBasket)
        {
            var response = await _httpClient.PostAsJsonAsync("baskets", testBasket);
            return response.IsSuccessStatusCode;
        }
    }
}
