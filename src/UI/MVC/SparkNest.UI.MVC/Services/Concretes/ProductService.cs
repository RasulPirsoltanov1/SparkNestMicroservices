using SparkNest.Common.DTOs;
using SparkNest.UI.MVC.Models;
using SparkNest.UI.MVC.Models.Product;
using SparkNest.UI.MVC.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace SparkNest.UI.MVC.Services.Concretes
{
    public class ProductService : IProductService
    {
        HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ProductVM>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetAsync("product");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var successResponse = await response.Content.ReadFromJsonAsync<Response<List<ProductVM>>>();
            return successResponse.Data;
        }

        public async Task<List<CategoryVM>> GetAllCateegoryAsync()
        {
            var response = await _httpClient.GetAsync("categories");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var successResponse = await response.Content.ReadFromJsonAsync<Response<List<CategoryVM>>>();
            return successResponse.Data;
        }


        public async Task<List<ProductVM>> GetAllProductsByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"product/user/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var successResponse = await response.Content.ReadFromJsonAsync<Response<List<ProductVM>>>();
            return successResponse.Data;
        }

        public async Task<ProductVM> GetByProductId(string productId)
        {
            if (productId == null)
                return null;
            var response = await _httpClient.GetAsync($"product/{productId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var successResponse = await response.Content.ReadFromJsonAsync<Response<ProductVM>>();
            return successResponse.Data;
        }
        public async Task<bool> CreateProductAsync(ProductCreateVM product)
        {
            var response = await _httpClient.PostAsJsonAsync<ProductCreateVM>($"product", product);
            return response.IsSuccessStatusCode;
        }

        public async  Task<bool> DeleteAsync(string productId)
        {

            var response = await _httpClient.DeleteAsync($"product/{productId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProductAsync(ProductUpdateVM product)
        {
            var response = await _httpClient.PutAsJsonAsync<ProductUpdateVM>($"product", product);
            return response.IsSuccessStatusCode;
        }
    }
}
