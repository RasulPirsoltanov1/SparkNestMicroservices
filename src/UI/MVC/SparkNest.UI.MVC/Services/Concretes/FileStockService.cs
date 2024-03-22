using SparkNest.UI.MVC.Models.Files;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Services.Concretes
{

    public class FileStockService : IFileStockService
    {
        HttpClient _httpClient;

        public FileStockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<bool> DeletePhoto(string photoPath)
        {
            throw new NotImplementedException();
        }

        public Task<PhotoVM> UploadPhoto(IFormFile photo)
        {
            if(photo == null || photo.Length<=0)
                return null;

            throw new NotImplementedException();
        }
    }
}
