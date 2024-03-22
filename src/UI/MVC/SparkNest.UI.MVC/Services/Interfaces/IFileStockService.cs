using SparkNest.UI.MVC.Models.Files;

namespace SparkNest.UI.MVC.Services.Interfaces
{
    public interface IFileStockService
    {
        Task<PhotoVM> UploadPhoto(IFormFile photo);
        Task<Boolean> DeletePhoto(string photoPath);
    }
}
