using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using SparkNest.Common.Base.Services;
using SparkNest.UI.MVC.Models;
using SparkNest.UI.MVC.Services.Interfaces;
using System.Net.Http;

namespace SparkNest.UI.MVC.Services.Concretes
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {
        ISharedIdentityService _sharedIdentityService;
        ServiceApiSettings _serviceApiSettings;
        ClientSettings _clientSettings;
        IClientAccessTokenCache _clientAccessTokenCache;
        HttpClient _httpClient;

        public ClientCredentialTokenService(ISharedIdentityService sharedIdentityService, ServiceApiSettings serviceApiSettings, ClientSettings clientSettings, IClientAccessTokenCache clientAccessTokenCache, HttpClient httpClient)
        {
            _sharedIdentityService = sharedIdentityService;
            _serviceApiSettings = serviceApiSettings;
            _clientSettings = clientSettings;
            _clientAccessTokenCache = clientAccessTokenCache;
            _httpClient = httpClient;
        }

        public async Task<string> GetTokenAsync()
        {
            var currentToken = await _clientAccessTokenCache.GetAsync("WebClient", parameters: null, cancellationToken: default);
            if (currentToken != null)
            {
                return currentToken.AccessToken;
            }
            var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityBaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (discovery.IsError)
            {
                throw discovery.Exception;
            }

            var tokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = _clientSettings.WebClient.ClientId,
                ClientSecret = _clientSettings.WebClient.ClientSecret,
                Address= discovery.TokenEndpoint
            };

            var newToken = await _httpClient.RequestClientCredentialsTokenAsync(tokenRequest);
            if(newToken.IsError)
            {
                throw newToken.Exception;
            }
           await _clientAccessTokenCache.SetAsync("WebClient", newToken.AccessToken, newToken.ExpiresIn,parameters:null);
            return newToken.AccessToken;
        }
    }
}
