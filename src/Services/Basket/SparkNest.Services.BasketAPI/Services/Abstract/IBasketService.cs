using SparkNest.Common.DTOs;
using SparkNest.Services.BasketAPI.DTOs;

namespace SparkNest.Services.BasketAPI.Services.Abstract
{
    public interface IBasketService
    {
        Task<Response<BasketDTO>> GetBasketAsync(string userId);
        Task<Response<bool>> SaveOrUpdateAsync(BasketDTO basketDTO);
        Task<Response<bool>> DeleteAsync(string userId);
    }
}
