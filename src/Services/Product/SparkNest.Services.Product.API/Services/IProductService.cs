using SparkNest.Common.DTOs;
using SparkNest.Services.ProductAPI.DTOs;

namespace SparkNest.Services.ProductAPI.Services
{
    public interface IProductService
    {
        Task<Response<ProductDTO>> CreateAsync(ProductCreateDTO productCreateDTO);
        Task<Response<NoContent>> DeleteAsync(string productId);
        Task<Response<List<ProductDTO>>> GetAllAsync();
        Task<Response<List<ProductDTO>>> GetAllByUserIdAsync(string UserId);
        Task<Response<ProductDTO>> GetByIdAsync(string Id);
        Task<Response<NoContent>> UpdateAsync(ProductUpdateDTO productUpdateDTO);
    }
}