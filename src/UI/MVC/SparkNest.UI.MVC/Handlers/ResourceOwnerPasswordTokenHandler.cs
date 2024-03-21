using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using SparkNest.UI.MVC.Exceptions;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Handlers
{
    public class ResourceOwnerPasswordTokenHandler : DelegatingHandler
    {
        ILogger<ResourceOwnerPasswordTokenHandler> _logger;
        IHttpContextAccessor _httpContextAccessor;
        IIdentityService _identityService;

        public ResourceOwnerPasswordTokenHandler(ILogger<ResourceOwnerPasswordTokenHandler> logger, IHttpContextAccessor contextAccessor, IIdentityService identityService)
        {
            _logger = logger;
            _httpContextAccessor = contextAccessor;
            _identityService = identityService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var tokenResponse = await _identityService.GetAccessTokenByRefreshToken();
                if(tokenResponse != null)
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
                    response = await base.SendAsync(request,cancellationToken);
                }
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnAuthorizeException();
                }
            }
            return response;
        }
    }
}
