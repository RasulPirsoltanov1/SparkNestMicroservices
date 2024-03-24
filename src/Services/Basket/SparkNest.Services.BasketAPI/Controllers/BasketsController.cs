using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkNest.Common.Base.Services;
using SparkNest.Common.ControllerBases;
using SparkNest.Services.BasketAPI.DTOs;
using SparkNest.Services.BasketAPI.Services.Abstract;

namespace SparkNest.Services.BasketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomControllerBase
    {
        IBasketService _basketService;
        ISharedIdentityService _sharedIdentityService;

        public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var claims = HttpContext.User.Claims;
            return Ok(await _basketService.GetBasketAsync(_sharedIdentityService.UserId));
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDTO)
        {
            basketDTO.UserId = _sharedIdentityService.UserId;
            var response = await _basketService.SaveOrUpdateAsync(basketDTO);
            return CreateActionResultInstance(response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
        {
            return Ok(await _basketService.DeleteAsync(_sharedIdentityService.UserId));
        }
    }
}
