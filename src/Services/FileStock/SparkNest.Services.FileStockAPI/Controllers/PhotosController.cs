using FileUploadExtensionForIFormFile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkNest.Common.ControllerBases;
using SparkNest.Common.DTOs;
using SparkNest.Services.FileStockAPI.DTOs;
using System.IO;

namespace SparkNest.Services.FileStockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SavePhoto(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo != null && photo.Length > 0)
            {
                var path = await photo.UploadFileToAsync("photos");
                PhotoDTO photoDTO = new PhotoDTO()
                {
                    Url = path
                };
                return CreateActionResultInstance(Response<PhotoDTO>.Success(photoDTO, 200));
            }
            return CreateActionResultInstance<NoContent>(Response<NoContent>.Fail("photo is empty", 400));
        }
        [HttpGet("{photoUrl}")]
        public async Task<IActionResult> PhotoDelete(string photoUrl)
        {
            try
            {
                await IFormFileExtension.DeleteFileAsync(photoUrl);
                return CreateActionResultInstance<NoContent>(Response<NoContent>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResultInstance<NoContent>(Response<NoContent>.Fail(ex.Message,404));
            }
        }
    }
}
