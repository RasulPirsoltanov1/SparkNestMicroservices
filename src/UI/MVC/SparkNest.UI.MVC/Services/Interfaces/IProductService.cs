using SparkNest.UI.MVC.Controllers;
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
    }
}
