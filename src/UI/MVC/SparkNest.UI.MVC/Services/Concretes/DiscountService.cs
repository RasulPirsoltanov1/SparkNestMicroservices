using SparkNest.Common.DTOs;
using SparkNest.UI.MVC.Models.Discount;
using SparkNest.UI.MVC.Services.Interfaces;
using System.Text.Json;

namespace SparkNest.UI.MVC.Services.Concretes
{
    public class DiscountService : IDiscountService
    {
        HttpClient _httpClient;

        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DiscountVM> GetDiscount(string code)
        {
            var response = await _httpClient.GetAsync($"discounts/GetByCode/{code}");
            if(!response.IsSuccessStatusCode)
            {
                return null;
            }
            var discount = await response.Content.ReadFromJsonAsync<Response<List<DiscountVM>>>();
            return discount.Data.FirstOrDefault() ;
        }
    }
}
