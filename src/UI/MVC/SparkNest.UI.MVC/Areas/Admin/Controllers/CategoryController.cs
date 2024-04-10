﻿using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Models.Product;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        IProductService _productService;

        public CategoryController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _productService.GetAllCateegoryAsync();
            return View(categories);
        }

        public async Task<IActionResult> Create() {
            ViewBag.Categories = await _productService.GetAllCateegoryAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryVM categoryVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _productService.GetAllCateegoryAsync();
                return View();
            }
            var result = await _productService.CreateCategoryAsync(categoryVM);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string? categoryId)
        {
            if(categoryId is null)
            {
                return BadRequest();
            }
            var result = await _productService.DeleteCategoryAsync(categoryId);
            return Ok(result);
        }
    }
}
