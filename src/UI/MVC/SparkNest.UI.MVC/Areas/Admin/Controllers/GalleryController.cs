using Microsoft.AspNetCore.Mvc;
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
            request.PhotoUrl = request.PhotoUrl.Replace(@"http://localhost:2002/","");
            var result = await _productService.DeletePhotoAsync(request.Id, request.PhotoUrl);
            return Ok();
        }
    }

    public class DeletePhotoRequest
    {
        public string Id { get; set; }
        public string PhotoUrl { get; set; }
    }


}
