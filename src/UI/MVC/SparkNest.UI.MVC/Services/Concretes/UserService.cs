using SparkNest.UI.MVC.Models;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Services.Concretes
{
    public class UserService: IUserService
    {
        HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserVM> GetUser()
        {
           return await _httpClient.GetFromJsonAsync<UserVM>("api/user/getuser");
        }
    }
}
