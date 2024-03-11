using SparkNest.Common.DTOs;
using SparkNest.Services.DiscountAPI.Models;

namespace SparkNest.Services.DiscountAPI.Services
{
    public interface IDiscountService
    {
        Task<Response<List<Discount>>> GetAllAsync();
        Task<Response<Discount>> GetByIdAsync(int id);
        Task<Response<NoContent>> SaveAsync(Discount discount);
        Task<Response<NoContent>> UpdateAsync(Discount discount);
        Task<Response<NoContent>> DeleteAsync(int id);
        Task<Response<List<Discount>>> GetByCodeAndUserIdAsync(string code,string userId);
    }
}
