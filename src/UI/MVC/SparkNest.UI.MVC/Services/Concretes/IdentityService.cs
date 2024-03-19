using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using SparkNest.Common.Base.DTOs;
using SparkNest.Common.DTOs;
using SparkNest.UI.MVC.Models;
using SparkNest.UI.MVC.Services.Interfaces;
using System.Globalization;
using System.Security.Claims;
using System.Text.Json;

namespace SparkNest.UI.MVC.Services.Concretes
{
    public class IdentityService : IIdentityService
    {
        HttpClient _httpClient;
        IHttpContextAccessor _HttpContextAccessor;
        ClientSettings _clientSettings;
        ServiceApiSettings _serviceApiSettings;

        public IdentityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, ClientSettings clientSettings, ServiceApiSettings serviceApiSettings)
        {
            _httpClient = httpClient;
            _HttpContextAccessor = httpContextAccessor;
            _clientSettings = clientSettings;
            _serviceApiSettings = serviceApiSettings;
        }

        public async Task<Response<bool>> SignIn(SignInInput signInInput)
        {
            var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.BaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (discovery.IsError)
            {
                throw discovery.Exception;
            }

            var passwordTokenRequest = new PasswordTokenRequest
            {
                ClientId = _clientSettings.WebClientForUser.ClientId,
                ClientSecret = _clientSettings.WebClientForUser.ClientSecret,
                UserName = signInInput.Email,
                Address = discovery.TokenEndpoint,
                Password = signInInput.Password
            };

            var token = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);
            if (token.IsError)
            {
                var responseContent = await token.HttpResponse.Content.ReadAsStringAsync();
                var errorDTO = JsonSerializer.Deserialize<ErrorDTO>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return Response<bool>.Fail(errorDTO.Errors, 404);
            }

            var userInfoRequest = new UserInfoRequest
            {
                Token = token.AccessToken,
                Address = discovery.UserInfoEndpoint,
            };
            var userInfo = await _httpClient.GetUserInfoAsync(userInfoRequest);
            if (userInfo.IsError)
            {
                throw userInfo.Exception;
            }
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(userInfo.Claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var authenticationProperties = new AuthenticationProperties();
            authenticationProperties.StoreTokens(new List<AuthenticationToken> {
                new AuthenticationToken() {
                Name =OpenIdConnectParameterNames.AccessToken,
                Value = token.AccessToken
            },
                   new AuthenticationToken() {
                Name =OpenIdConnectParameterNames.RefreshToken,
                Value = token.RefreshToken
            },
                      new AuthenticationToken() {
                Name =OpenIdConnectParameterNames.ExpiresIn,
                Value = DateTime.Now.AddSeconds(token.ExpiresIn).ToString("O",CultureInfo.InvariantCulture)
            }
            });
            authenticationProperties.IsPersistent = signInInput.IsRememberMe;
            await _HttpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);
            return Response<bool>.Success(true, 200);
        }
        public async Task<TokenResponse> GetAccessTokenByRefreshToken()
        {
            var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.BaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (discovery.IsError)
            {
                throw discovery.Exception;
            }
            var refreshToken = await _HttpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            RefreshTokenRequest refreshTokenRequest = new RefreshTokenRequest
            {
                Address = discovery.TokenEndpoint,
                ClientId = _clientSettings.WebClientForUser.ClientId,
                ClientSecret = _clientSettings.WebClientForUser.ClientSecret,
                RefreshToken = refreshToken
            };
            var token = await _httpClient.RequestRefreshTokenAsync(refreshTokenRequest);
            if (token.IsError)
            {
                return null;
            }
            var authenticationTokens = new List<AuthenticationToken> {
                new AuthenticationToken() {
                Name =OpenIdConnectParameterNames.AccessToken,
                Value = token.AccessToken
            },
                   new AuthenticationToken() {
                Name =OpenIdConnectParameterNames.RefreshToken,
                Value = token.RefreshToken
            },
                      new AuthenticationToken() {
                Name =OpenIdConnectParameterNames.ExpiresIn,
                Value = DateTime.Now.AddSeconds(token.ExpiresIn).ToString("O",CultureInfo.InvariantCulture)
            }
            };
            var authenticationResult = await _HttpContextAccessor.HttpContext.AuthenticateAsync();
            var properties = authenticationResult.Properties;
            properties.StoreTokens(authenticationTokens);
            await _HttpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, authenticationResult.Principal, properties);
            return token;
        }

        public async Task RevokeRefreshToken()
        {
            var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.BaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (discovery.IsError)
            {
                throw discovery.Exception;
            }
            var refreshToken = await _HttpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            TokenRevocationRequest tokenRevocationRequest = new TokenRevocationRequest
            {
                Address = discovery.RevocationEndpoint,
                ClientId = _clientSettings.WebClientForUser.ClientId,
                ClientSecret = _clientSettings.WebClientForUser.ClientSecret,
                Token = refreshToken,
                TokenTypeHint = "refresh_token"
            };
            await _httpClient.RevokeTokenAsync(tokenRevocationRequest);
        }

    }
}
