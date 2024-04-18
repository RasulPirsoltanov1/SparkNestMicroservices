using Microsoft.AspNetCore.Mvc;
using SparkNest.Common.ControllerBases;
using SparkNest.Services.ProductAPI.DTOs;
using SparkNest.Services.ProductAPI.Services;

namespace SparkNest.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : CustomControllerBase
    {
        IProductService _productService;

        public RatingController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> RateProduct(ProductRateDTO productRateDTO)
        {
            var res = await _productService.RateProductAsync(productRateDTO);
            return CreateActionResultInstance(res);
        }
    }
}
