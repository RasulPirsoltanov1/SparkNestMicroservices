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
            try
            {
                var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
                {
                    Address = _serviceApiSettings.IdentityBaseUri,
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

                var claims = new List<Claim>();

                // Adding claims from userInfo.Claims
                claims.AddRange(userInfo.Claims);

                // Adding UserId claim if present
                var userIdClaim = userInfo.Claims.FirstOrDefault(c => c.Type == "sub");
                if (userIdClaim != null)
                {
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, userIdClaim.Value));
                }

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");
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
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }
        }




        public async Task<TokenResponse> GetAccessTokenByRefreshToken()
        {
            var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityBaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (discovery.IsError)
            {
                throw discovery.Exception;
            }
            var refreshToken = await _HttpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            if (refreshToken is null)
                return null;

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
                Address = _serviceApiSettings.IdentityBaseUri,
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

        public async Task<Response<bool>> SignUpAsync(SignUpInput signUpInput)
        {
            try
            {
                // Obtain a token using client credentials
                var clientCredentialToken = await GetClientCredentialsTokenAsync();

                // Use the obtained token in the request headers
                _httpClient.SetBearerToken(clientCredentialToken.AccessToken);

                // Proceed with the signup process
                var response = await _httpClient.PostAsJsonAsync(_serviceApiSettings.IdentityBaseUri + "/api/user/signup", signUpInput);
                var result = await response.Content.ReadFromJsonAsync<Response<bool>>();
                return result;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }
        }

        private async Task<TokenResponse> GetClientCredentialsTokenAsync()
        {
            var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityBaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (discovery.IsError)
            {
                throw discovery.Exception;
            }

            var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                Address = discovery.TokenEndpoint,
                ClientId = _clientSettings.WebClient.ClientId,
                ClientSecret = _clientSettings.WebClient.ClientSecret,
            };

            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);
            if (tokenResponse.IsError)
            {
                throw new Exception($"Error while obtaining client credentials token: {tokenResponse.Error}");
            }

            return tokenResponse;
        }

    }
}
