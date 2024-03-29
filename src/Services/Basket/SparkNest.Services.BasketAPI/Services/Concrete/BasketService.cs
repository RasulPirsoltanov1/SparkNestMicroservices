using SparkNest.Common.Base.Services;
using SparkNest.Common.DTOs;
using SparkNest.Services.BasketAPI.DTOs;
using SparkNest.Services.BasketAPI.Services.Abstract;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SparkNest.Services.BasketAPI.Services.Concrete
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketService(RedisService redisService, ISharedIdentityService sharedIdentityService)
        {
            _redisService = redisService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<Response<bool>> DeleteAsync(string userId)
        {
            var result = await _redisService.GetDb().KeyDeleteAsync(userId);
            if (!result)
            {
                return Response<bool>.Fail("Basket could not be deleted.", 500);
            }
            return Response<bool>.Success(200);
        }

        public async Task<Response<BasketDto>> GetBasketAsync(string userId)
        {
            var existingBasket = await _redisService.GetDb().StringGetAsync(userId);
            if (string.IsNullOrEmpty(existingBasket))
            {
                return Response<BasketDto>.Fail("Basket not found!", 404);
            }
            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existingBasket), 200);
        }

        public string GetUserId()
        {
            return _sharedIdentityService.UserId;
        }

        public async Task<Response<bool>> SaveOrUpdateAsync(BasketDto basketDTO)
        {
            //var existingBasket = await GetBasketAsync(_sharedIdentityService.UserId);
            //if (existingBasket.Data != null)
            //{
            //    foreach (var item in basketDTO.basketItems) // basketDTO.basketItems deki elemntlerin icleri neden null olarak geliyor?
            //    {
            //        var existingItem = existingBasket.Data.basketItems.FirstOrDefault(x => x.ProductId == item.ProductId);
            //        if (existingItem != null)
            //        {
            //            existingBasket.Data.basketItems.FirstOrDefault(x => x.ProductId == item.ProductId).Quantity += item.Quantity == 0 ? 1 : item.Quantity;
            //        }
            //        else
            //        {
            //            existingBasket.Data.basketItems.Add(item);
            //        }
            //    }
            //    basketDTO = existingBasket.Data;
            //}

            var status = await _redisService.GetDb().StringSetAsync(_sharedIdentityService.UserId??basketDTO.UserId, JsonSerializer.Serialize(basketDTO));

            return status ? Response<bool>.Success(200) : Response<bool>.Fail("Basket could not be saved or updated.", 500);
        }
    }
}
