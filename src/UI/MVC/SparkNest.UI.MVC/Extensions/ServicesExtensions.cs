using SparkNest.UI.MVC.Handlers;
using SparkNest.UI.MVC.Models;
using SparkNest.UI.MVC.Services.Concretes;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
            var serviceApiSettings = services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpClient<IUserService, UserService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);

            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);
            });

            services.AddHttpClient<IFileStockService, FileStockService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.BaseUri + serviceApiSettings.FileStockUriPath);
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IProductService, ProductService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.BaseUri + serviceApiSettings.ProductUriPath);
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IBasketService, BasketService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.BaseUri + serviceApiSettings.BasketUriPath);

            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IDiscountService, DiscountService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.BaseUri + serviceApiSettings.DiscountPath);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
            return services;
        }
    }
}
