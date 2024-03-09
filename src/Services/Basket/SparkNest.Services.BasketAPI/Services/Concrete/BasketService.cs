using SparkNest.Common.Base.Services;
using SparkNest.Common.DTOs;
using SparkNest.Services.BasketAPI.DTOs;
using SparkNest.Services.BasketAPI.Services.Abstract;
using System.Text.Json;

namespace SparkNest.Services.BasketAPI.Services.Concrete
{
    public class BasketService : IBasketService
    {
        RedisService _redisService;
        ISharedIdentityService _sharedIdentityService;

        public BasketService(RedisService redisService, ISharedIdentityService sharedIdentityService)
        {
            this._redisService = redisService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<Response<bool>> DeleteAsync(string userId)
        {
            var result = await _redisService.GetDb().KeyDeleteAsync(userId);
            if (!result)
            {
                return Response<bool>.Fail("basket could not delete.", 500);
            }
            return Response<bool>.Success(200);

        }

        public async Task<Response<BasketDTO>> GetBasketAsync(string userId)
        {
            var exisDb = await _redisService.GetDb().StringGetAsync(userId);
            if (string.IsNullOrEmpty(exisDb))
            {
                return Response<BasketDTO>.Fail("Basket not found!", 404);
            }
            return Response<BasketDTO>.Success(JsonSerializer.Deserialize<BasketDTO>(exisDb), 200);
        }

        public async Task<Response<bool>> SaveOrUpdateAsync(BasketDTO basketDTO)
        {
            var status = await _redisService.GetDb().StringSetAsync(_sharedIdentityService.UserId, JsonSerializer.Serialize(basketDTO));

            return status ? Response<bool>.Success(200) : Response<bool>.Fail("Basket could not save or update", 500);
        }
    }
}
