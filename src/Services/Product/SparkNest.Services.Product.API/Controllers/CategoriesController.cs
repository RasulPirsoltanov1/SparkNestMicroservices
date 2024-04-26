using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkNest.Common.ControllerBases;
using SparkNest.Services.ProductAPI.DTOs;
using SparkNest.Services.ProductAPI.Models;
using SparkNest.Services.ProductAPI.Services;

namespace SparkNest.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CustomControllerBase
    {
        ICategoryService _categoryService;
        IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _categoryService.GetAllAsync());
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(string Id)
        {
            return Ok(await _categoryService.GetByIdAsync(Id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CategoryDTO categoryDTO)
        {
            return Ok(await _categoryService.CreateAsync(categoryDTO));
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteAsync(string categoryId)
        {
            return Ok(await _categoryService.DelteAsync(categoryId));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CategoryDTO categoryDTO)
        {
            return Ok(await _categoryService.UpdateAsync(categoryDTO));
        }
    }
}
