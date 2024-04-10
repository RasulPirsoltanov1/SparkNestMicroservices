using SparkNest.Common.DTOs;
using SparkNest.Services.ProductAPI.DTOs;
using SparkNest.Services.ProductAPI.Models;

namespace SparkNest.Services.ProductAPI.Services
{
    public interface ICategoryService
    {
        Task<Response<CategoryDTO>> CreateAsync(CategoryDTO categoryDTO);
        Task<Response<bool>> DelteAsync(string categoryId);
        Task<Response<List<CategoryDTO>>> GetAllAsync();
        Task<Response<CategoryDTO>> GetByIdAsync(string Id);
    }
}