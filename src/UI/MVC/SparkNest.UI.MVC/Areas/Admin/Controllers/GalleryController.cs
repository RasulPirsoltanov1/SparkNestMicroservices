using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Models.Gallery;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GalleryController : Controller
    {
        IProductService _productService;

        public GalleryController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public async Task<IActionResult> DeletePhoto(DeletePhotoRequest request)
        {
            request.PhotoUrl = request.PhotoUrl.Replace(@"http://localhost:2002/", "");
            var result = await _productService.DeletePhotoAsync(request.Id, request.PhotoUrl);
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddPhotosToGallery(AddPhotosRequest request)
        {
            var result = await _productService.AddPhotosToGalleryAsync(request);
            return RedirectToAction("Update", "Products", new
            {
                id = request.ProductId,
            });
        }
    }
}
