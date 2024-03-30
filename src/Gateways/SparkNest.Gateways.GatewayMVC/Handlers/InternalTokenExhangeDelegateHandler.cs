//using IdentityModel.Client;

//namespace SparkNest.Gateways.GatewayMVC.Handlers
//{
//    public class InternalTokenExhangeDelegateHandler : DelegatingHandler
//    {
//        private readonly HttpClient _httpClient;
//        private readonly IConfiguration _configuration;

//        private string _accessToken;

//        public InternalTokenExhangeDelegateHandler(HttpClient httpClient, IConfiguration configuration)
//        {
//            _httpClient = httpClient;
//            _configuration = configuration;
//        }

//        private async Task<string> GetToken(string requestToken)
//        {
//            if (!string.IsNullOrEmpty(_accessToken))
//            {
//                return _accessToken;
//            }

//            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
//            {
//                Address = _configuration["IdentityServerURL"],
//                Policy = new DiscoveryPolicy { RequireHttps = false }
//            });

//            if (disco.IsError)
//            {
//                throw disco.Exception;
//            }

//            TokenExchangeTokenRequest tokenExchangeTokenRequest = new TokenExchangeTokenRequest()
//            {
//                Address = disco.TokenEndpoint,
//                ClientId = _configuration["ClientId"],
//                ClientSecret = _configuration["ClientSecret"],
//                GrantType = _configuration["TokenGrantType"],
//                SubjectToken = requestToken,
//                SubjectTokenType = "urn:ietf:params:oauth:token-type:access-token",
//                Scope = "openid discount_full_permission fakepayment_full_permission"
//            };

//            var tokenResponse = await _httpClient.RequestTokenExchangeTokenAsync(tokenExchangeTokenRequest);

//            if (tokenResponse.IsError)
//            {
//                throw tokenResponse.Exception;
//            }

//            _accessToken = tokenResponse.AccessToken;

//            return _accessToken;
//        }

//        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
//        {
//            var requestToken = request.Headers.Authorization.Parameter;

//            var newToken = await GetToken(requestToken);

//            request.SetBearerToken(newToken);

//            return await base.SendAsync(request, cancellationToken);
//        }
//    }
//}
