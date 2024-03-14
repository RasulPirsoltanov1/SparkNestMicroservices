using IdentityModel.Client;
using SparkNest.Common.DTOs;
using SparkNest.UI.MVC.Models;

namespace SparkNest.UI.MVC.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SignInInput signInInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();
    }
}
