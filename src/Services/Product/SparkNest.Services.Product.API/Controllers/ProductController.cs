using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkNest.Common.ControllerBases;
using SparkNest.Services.ProductAPI.DTOs;
using SparkNest.Services.ProductAPI.Services;

namespace SparkNest.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : CustomControllerBase
    {
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateActionResultInstance(await _productService.GetAllAsync());
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(string Id)
        {
            return CreateActionResultInstance(await _productService.GetByIdAsync(Id));
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserIdAsync(string userId)
        {
            return CreateActionResultInstance(await _productService.GetAllByUserIdAsync(userId));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductCreateDTO productCreateDTO)
        {
            return CreateActionResultInstance(await _productService.CreateAsync(productCreateDTO));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(ProductUpdateDTO productUpdateDTO)
        {
            return CreateActionResultInstance(await _productService.UpdateAsync(productUpdateDTO));
        }
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteAsync(string productId)
        {
            return CreateActionResultInstance(await _productService.DeleteAsync(productId));
        }
    }
}
