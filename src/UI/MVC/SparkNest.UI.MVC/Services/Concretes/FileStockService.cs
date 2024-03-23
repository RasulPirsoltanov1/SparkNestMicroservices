using SparkNest.Common.DTOs;
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

        public async Task<bool> DeletePhoto(string photoPath)
        {
            if (photoPath == null)
            {
                return false;
            }
            var response = await _httpClient.DeleteAsync($"photos/{photoPath}");
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        public async Task<PhotoVM> UploadPhoto(IFormFile photo)
        {
            if (photo == null || photo.Length <= 0)
                return null;
            using (var memoryStream = new MemoryStream())
            {
                photo.CopyTo(memoryStream);
                var multiPartContent = new MultipartFormDataContent();
                multiPartContent.Add(new ByteArrayContent(memoryStream.ToArray()), "photo", photo.FileName);
                var response = await _httpClient.PostAsync("photos", multiPartContent);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                return (await response.Content.ReadFromJsonAsync<Response<PhotoVM>>()).Data;
            }
            throw new NotImplementedException();
        }
    }
}
