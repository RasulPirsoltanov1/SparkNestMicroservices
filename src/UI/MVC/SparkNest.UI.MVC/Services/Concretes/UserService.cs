using SparkNest.UI.MVC.Application.DTOs;
using SparkNest.UI.MVC.Helpers;
using SparkNest.UI.MVC.Models;
using SparkNest.UI.MVC.Services.Interfaces;
using System.Net.Http.Json;

namespace SparkNest.UI.MVC.Services.Concretes
{
    public class UserService : IUserService
    {
        HttpClient _httpClient;
        IFileStockService _fileStockService;
        FileStockHelper _fileStockHelper;
        public UserService(HttpClient httpClient, IFileStockService fileStockService, FileStockHelper fileStockHelper)
        {
            _httpClient = httpClient;
            _fileStockService = fileStockService;
            _fileStockHelper = fileStockHelper;
        }

        public async Task<UserVM> GetUser()
        {
            var user = await _httpClient.GetFromJsonAsync<UserVM>("api/user/getuser");
            user.ImageFileStockUrl = _fileStockHelper.GetFileStockUrl(user.ImageUrl);
            return user;
        }

        public async Task<bool> UploadImageAsync(UploadProfileImageDTO uploadProfileImageDTO)
        {

            if(uploadProfileImageDTO.Image == null)
            {
                return false;
            }

            var imageUrl = await _fileStockService.UploadPhoto(uploadProfileImageDTO.Image);

            uploadProfileImageDTO.ImageUrl = imageUrl.Url;

            var response = await _httpClient.PostAsJsonAsync("api/user/UploadImage", uploadProfileImageDTO);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

    }
}
