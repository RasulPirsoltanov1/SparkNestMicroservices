using SparkNest.Common.Base.Services;
using SparkNest.Common.DTOs;
using SparkNest.UI.MVC.Models.Baskets;
using SparkNest.UI.MVC.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace SparkNest.UI.MVC.Services.Concretes
{
    public class BasketService : IBasketService
    {
        HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SaveOrUpdate(BasketVM basketVM)
        {
            var jsonContent = JsonSerializer.Serialize(basketVM);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("basketss", content);

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
            if (basket is null)
            {
                return false;
            }
            var deleteBasketItem = basket.basketItems.FirstOrDefault(x => x.ProductId == productId);
            if (deleteBasketItem == null)
                return false;
            var deleteBasketResult = basket.basketItems.Remove(deleteBasketItem);
            if (!deleteBasketResult)
            {
                return false;
            }
            if (basket.basketItems.Any())
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
                var existingBasketItem = basket.basketItems.FirstOrDefault(x => x.ProductId == basketItemVM.ProductId);
                if (existingBasketItem != null)
                {
                    existingBasketItem.Quantity += 1;
                }
                else
                {
                    basket.basketItems.Add(basketItemVM);
                }
            }
            else
            {
                basket = new BasketVM();
                basket.basketItems.Add(basketItemVM);
            }

            await SaveOrUpdate(basket);
        }

        public Task<bool> ApplyDicount(string discountCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CancelApplyDicount()
        {
            throw new NotImplementedException();
        }


    }
}
