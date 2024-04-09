using SparkNest.Common.DTOs;
using SparkNest.UI.MVC.Controllers;
using SparkNest.UI.MVC.Helpers;
using SparkNest.UI.MVC.Models;
using SparkNest.UI.MVC.Models.Files;
using SparkNest.UI.MVC.Models.Gallery;
using SparkNest.UI.MVC.Models.Product;
using SparkNest.UI.MVC.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;

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
                PhotoUrl = x.PhotoUrl,
                PhotoFileStockUrl = _fileStockHelper.GetFileStockUrl(x.PhotoUrl),
                Price = x.Price,
                UserId = x.UserId
            }).ToList();
            foreach (var item in data)
            {
                foreach (var item1 in item.PhotoUrls)
                {
                    item.PhotoFileStockUrls.Add(_fileStockHelper.GetFileStockUrl(item1));
                }
            }
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
            foreach (var item1 in data.PhotoUrls)
            {
                data.PhotoFileStockUrls.Add(_fileStockHelper.GetFileStockUrl(item1));
            }
            return data;
        }
        public async Task<bool> CreateProductAsync(ProductCreateVM product)
        {
            if (product.Photos is not null)
            {
                foreach (var item in product.Photos)
                {
                    PhotoVM? responses = await _fileStockService.UploadPhoto(item);
                    product.PhotoUrls.Add(responses.Url);
                }
            }
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

        /// <summary>
        /// Delete photo from gallery
        /// </summary>
        /// <param name="productId"> id of product </param>
        /// <param name="photoUrl">url path of photo</param>
        /// <returns></returns>
        public async Task<bool> DeletePhotoAsync(string productId, string photoUrl)
        {
            var dbProduct = await GetByProductId(productId);
            if (photoUrl != null)
            {
               
                var photoUrlPath = photoUrl.Replace("uploads\\photos\\", "");
                var RES = await _fileStockService.DeletePhoto(photoUrlPath);
            }
            var value = ExtractGUID(photoUrl);
            var response = await _httpClient.DeleteAsync($"product/{productId}/{value}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddPhotosToGalleryAsync(AddPhotosRequest addPhotosRequest)
        {
            List<string> photoUrls = new List<string>();
            if (addPhotosRequest.Photos is not null)
            {
                foreach (var item in addPhotosRequest.Photos)
                {
                    PhotoVM? responses = await _fileStockService.UploadPhoto(item);
                    photoUrls.Add(responses.Url);
                }
            }
            var response = await _httpClient.PostAsJsonAsync($"product/AddPhotosToGallery", new
            {
                productId = addPhotosRequest.ProductId,
                photoUrl = photoUrls
            });
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProductAsync(ProductUpdateVM product)
        {
            var dbProduct = await GetByProductId(product.Id);
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
            else
            {
                product.PhotoUrl = dbProduct.PhotoUrl;
            }
            var result = await _httpClient.PutAsJsonAsync<ProductUpdateVM>($"product", product);
            return result.IsSuccessStatusCode;
        }


        static string ExtractGUID(string url)
        {
            // Define the pattern for finding GUID-like strings
            string pattern = @"([a-fA-F0-9]{8}(-[a-fA-F0-9]{4}){3}-[a-fA-F0-9]{12})";

            // Search for the pattern in the URL
            Match match = Regex.Match(url, pattern);

            // If a match is found, return the GUID part
            if (match.Success)
            {
                return match.Value;
            }

            // Return empty string if no match is found
            return "";
        }
    }
}
