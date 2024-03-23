﻿using SparkNest.Common.DTOs;
using SparkNest.UI.MVC.Helpers;
using SparkNest.UI.MVC.Models;
using SparkNest.UI.MVC.Models.Files;
using SparkNest.UI.MVC.Models.Product;
using SparkNest.UI.MVC.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace SparkNest.UI.MVC.Services.Concretes
{
    public class ProductService : IProductService
    {
        HttpClient _httpClient;
        IFileStockService _fileStockService;
        FileStockHelper _fileStockHelper;
        ServiceApiSettings _serviceApiSettings;

        public ProductService(HttpClient httpClient, IFileStockService fileStockService, FileStockHelper fileStockHelper, ServiceApiSettings serviceApiSettings)
        {
            _httpClient = httpClient;
            _fileStockService = fileStockService;
            _fileStockHelper = fileStockHelper;
            _serviceApiSettings = serviceApiSettings;
        }
        public async Task<List<ProductVM>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetAsync("product");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var successResponse = await response.Content.ReadFromJsonAsync<Response<List<ProductVM>>>();
            var data = successResponse.Data.Select(x => new ProductVM
            {
                Category = x.Category,
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                CreatedDate = x.CreatedDate,
                Description = x.Description,
                Feature = x.Feature,
                PhotoUrl = x.PhotoUrl,
                PhotoFileStockUrl = _fileStockHelper.GetFileStockUrl(x.PhotoUrl),
                Price = x.Price,
                UserId = x.UserId
            }).ToList();
            return data;
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
            var data = successResponse.Data.Select(x => new ProductVM
            {
                Category = x.Category,
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                CreatedDate = x.CreatedDate,
                Description = x.Description,
                Feature = x.Feature,
                PhotoUrl =x.PhotoUrl,
                PhotoFileStockUrl = _fileStockHelper.GetFileStockUrl(x.PhotoUrl),
                Price = x.Price,
                UserId = x.UserId
            }).ToList();
            return data;
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
            var data = successResponse.Data;
            data.PhotoFileStockUrl = _fileStockHelper.GetFileStockUrl(data.PhotoUrl);
            return data;
        }
        public async Task<bool> CreateProductAsync(ProductCreateVM product)
        {
            PhotoVM? response = await _fileStockService.UploadPhoto(product.Photo);
            if (response == null)
            {
                return false;
            }
            product.PhotoUrl = response.Url;
            var result = await _httpClient.PostAsJsonAsync<ProductCreateVM>($"product", product);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string productId)
        {

            var response = await _httpClient.DeleteAsync($"product/{productId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProductAsync(ProductUpdateVM product)
        {
            if (product.Photo != null)
            {
                PhotoVM? response = await _fileStockService.UploadPhoto(product.Photo);
                if (response == null)
                {
                    return false;
                }
                product.PhotoUrl = response.Url;
                var prod = await GetByProductId(product.Id);
                var photoPath = prod.PhotoFileStockUrl.Replace(@"http://localhost:2002/uploads\photos\", "");
                var RES = await _fileStockService.DeletePhoto(photoPath);
            }
            var result = await _httpClient.PutAsJsonAsync<ProductUpdateVM>($"product", product);
            return result.IsSuccessStatusCode;
        }
    }
}
