﻿using SparkNest.UI.MVC.Application.DTOs.Produtcs;
using SparkNest.UI.MVC.Controllers;
using SparkNest.UI.MVC.Models.Gallery;
using SparkNest.UI.MVC.Models.Product;

namespace SparkNest.UI.MVC.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetAllProductsAsync();
        Task<List<CategoryVM>> GetAllCateegoryAsync();
        Task<List<ProductVM>> GetAllProductsByUserIdAsync(string userId);
        Task<bool> DeleteAsync(string productId);
        Task<bool> CreateProductAsync(ProductCreateVM product);
        Task<bool> UpdateProductAsync(ProductUpdateVM product);
        Task<ProductVM> GetByProductId(string productId);
        Task<bool> DeletePhotoAsync(string productId, string photoUrl);
        Task<bool> AddPhotosToGalleryAsync(AddPhotosRequest addPhotosRequest);
        Task<bool> CreateCategoryAsync(CategoryVM categoryVM);
        Task<bool> DeleteCategoryAsync(string categoryId);
        Task<bool> UpdateCategoryAsync(CategoryVM categoryVM);
        Task<bool> RateAsync(ProductRateDTO productRateDTO);
    }
}
