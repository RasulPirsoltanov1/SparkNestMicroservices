using SparkNest.Common.DTOs;
using SparkNest.Services.BlogAPI.Application.DTOs;
using SparkNest.Services.BlogAPI.Domain.Entities;

namespace SparkNest.Services.BlogAPI.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Response<bool>> CreateAsync(CategoryDTO category);
        Task<Response<bool>> DeleteAsync(string categoryId);
        Task<Response<bool>> DeletePhotoAsync(string categoryId, string photoUrl);
        Task<Response<List<CategoryDTO>>> GetAllAsync();
        Task<Response<CategoryDTO>> GetByIdAsync(string categoryId);
        Task<Response<NoContent>> UpdateAsync(CategoryUpdateDTO categoryUpdateDTO);
    }
}
