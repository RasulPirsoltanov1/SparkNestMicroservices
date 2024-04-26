using Microsoft.AspNetCore.Mvc;
using NuGet.Versioning;
using SparkNest.UI.MVC.Models.Products;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Controllers
{
    public class ProductsFilterController : Controller
    {
        IProductService _productService;

        public ProductsFilterController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(string? categoryId)
        {
            if (categoryId != null)
            {
                var products = (await _productService.GetAllProductsAsync()).Where(x => x.CategoryId == categoryId || x.Category?.SubCategoryId == categoryId).ToList();
                return View(products);
            }
            return View(await _productService.GetAllProductsAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(int sortBy)
        {
            ViewBag.SortBy = sortBy;
            var products = await _productService.GetAllProductsAsync();
            switch (sortBy)
            {
                case 1:
                    return View(products.OrderBy(x => x.Views).ToList());
                case 2:
                    return View(products.OrderBy(x => x.Price).ToList());
                case 3:
                    return View(products.OrderByDescending(x => x.Price).ToList());
                case 4:
                    return View(products.OrderByDescending(x => x.RatingCommon).ToList());
                case 5:
                    return View(products.OrderBy(x => x.Name).ToList());
                case 6:
                    return View(products.OrderByDescending(x => x.Name).ToList());
                default:
                    return View(products);
            }

        }
        [HttpPost("[controller]/filter")]
        public async Task<IActionResult> Index2(ProductsFilterVM productsFilterVM, params string[] CategoryIds)
        {
            // Minimum fiyat belirtilmişse
            if (productsFilterVM.MinAmount != null && productsFilterVM.MaxAmount != null)
            {
                // Tüm ürünleri al
                var products = await _productService.GetAllProductsAsync();

                decimal? minAmount = Convert.ToDecimal(productsFilterVM.MinAmount);
                decimal? maxAmount = Convert.ToDecimal(productsFilterVM.MaxAmount);

                products = products.Where(x =>
                    (decimal)(x.PriceDiscount == null ? x.Price : (decimal)x.PriceDiscount) >= minAmount &&
                    (decimal)(x.PriceDiscount == null ? x.Price : (decimal)x.PriceDiscount) <= maxAmount
                ).ToList();

                return View(products);
            }
            else
            {
                var allProducts = await _productService.GetAllProductsAsync();
                // Eğer kategori filtresi de varsa
                if (CategoryIds?.Any() == true)
                {
                    // Seçilen kategorilere göre filtrele
                    allProducts = allProducts.Where(p => CategoryIds.Any(x => x == p.Category.Id || x == p.Category.SubCategoryId)).ToList();
                }
                // Minimum fiyat belirtilmemişse, tüm ürünleri getir
                return View(allProducts);
            }
        }

    }
}
