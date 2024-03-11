using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkNest.Common.Base.Services;
using SparkNest.Common.ControllerBases;
using SparkNest.Services.DiscountAPI.Models;
using SparkNest.Services.DiscountAPI.Services;

namespace SparkNest.Services.DiscountAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomControllerBase
    {
        IDiscountService _discountService;
        ISharedIdentityService _sharedIdentityService;

        public DiscountsController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
        {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return CreateActionResultInstance(await _discountService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResultInstance(await _discountService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Discount discount)
        {
            return CreateActionResultInstance(await _discountService.SaveAsync(discount));
        }
        [HttpPut]
        public async Task<IActionResult> Put(Discount discount)
        {
            return CreateActionResultInstance(await _discountService.UpdateAsync(discount));
        }
        [HttpGet("GetByUserAndCode/{code}")]
        public async Task<IActionResult> GetByCodeAndUserId(string code)
        {
            return CreateActionResultInstance(await _discountService.GetByCodeAndUserIdAsync(code, _sharedIdentityService.UserId));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            return CreateActionResultInstance(await _discountService.DeleteAsync(id));
        }
    }
}
