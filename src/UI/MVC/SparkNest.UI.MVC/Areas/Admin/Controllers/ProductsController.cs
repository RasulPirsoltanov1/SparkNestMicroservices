﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SparkNest.Common.Base.Services;
using SparkNest.UI.MVC.Models.Product;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class ProductsController : Controller
    {
        IProductService _productService;
        ISharedIdentityService _sharedIdentityService;

        public ProductsController(IProductService productService, ISharedIdentityService sharedIdentityService)
        {
            _productService = productService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index(int currentPageIndex)
        {
            var productsAndPageCount = await GetProducts(currentPageIndex);
            var products = productsAndPageCount.Item1;
            var pageCount = productsAndPageCount.Item2;

            ViewBag.CurrentPageIndex = currentPageIndex;
            ViewBag.PageCount = pageCount;

            return View(products);
        }

        private async Task<(List<ProductVM>, int)> GetProducts(int currentPage)
        {
            int maxRows = 3;
            var products = (await _productService.GetAllProductsAsync()).ToList();
            int totalProductCount = products.Count;
            int pageCount = (int)Math.Ceiling((double)totalProductCount / maxRows);

            // Make sure currentPage is within valid range
            currentPage = currentPage < 1 ? 1 : currentPage;
            currentPage = currentPage > pageCount ? pageCount : currentPage;

            var paginatedProducts = products.Skip((currentPage - 1) * maxRows).Take(maxRows).ToList();

            return (paginatedProducts, pageCount);
        }

        public async Task<IActionResult> CreateAsync()
        {
            var categories = await _productService.GetAllCateegoryAsync();
            ViewBag.CategoryList = categories;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM productCreateVM)
        {
            var categories = await _productService.GetAllCateegoryAsync();
            ViewBag.CategoryList = categories;
            if (!ModelState.IsValid)
                return View(productCreateVM);
            productCreateVM.UserId = _sharedIdentityService.UserId;
            var response = await _productService.CreateProductAsync(productCreateVM);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var categories = await _productService.GetAllCateegoryAsync();
            ViewBag.CategoryList = categories;
            var product = await _productService.GetByProductId(id);
            if (product == null)
                return BadRequest();
            return View(new ProductUpdateVM
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                Description = product.Description,
                Feature = product.Feature,
                Name = product.Name,
                PhotoFileStockUrl = product.PhotoFileStockUrl,
                Price = product.Price,
                UserId = product.UserId,
                PhotoFileStockUrls = product.PhotoFileStockUrls,
                PriceDiscount = product.PriceDiscount,
                DiscountPercentage = product.DiscountPercentage
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateVM productUpdateVM)
        {
            var categories = await _productService.GetAllCateegoryAsync();
            ViewBag.CategoryList = categories;
            productUpdateVM.UserId = _sharedIdentityService.UserId;
            if (!ModelState.IsValid)
            {
                return View(productUpdateVM);
            }
            var response = await _productService.UpdateProductAsync(productUpdateVM);
            if (response)
                return RedirectToAction(nameof(Index));
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
