using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SparkNest.Common.Base.Services;
using SparkNest.UI.MVC.Extensions;
using SparkNest.UI.MVC.Handlers;
using SparkNest.UI.MVC.Helpers;
using SparkNest.UI.MVC.Models;
using SparkNest.UI.MVC.Services.Concretes;
using SparkNest.UI.MVC.Services.Interfaces;
using SparkNest.UI.MVC.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddSingleton<ServiceApiSettings>(provider =>
{
    var settings = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
    return settings;
});


builder.Services.AddSingleton<ClientSettings>(provider =>
{
    var settings = builder.Configuration.GetSection("ClientSettings").Get<ClientSettings>();
    return settings;
});


//HttpClient Interceptors
builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddScoped<ClientCredentialTokenHandler>();
builder.Services.AddHttpContextAccessor();

//Services Registration
var serviceApiASettings = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
builder.Services.AddClientAccessTokenManagement();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddScoped<IFileStockService, FileStockService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();


//Http services 
builder.Services.AddHttpClientServices();

//Cookie Authentication Configurations
builder.Services.AddAuthentication().AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Auth/SignIn";
    opt.ExpireTimeSpan = TimeSpan.FromDays(10);
    opt.SlidingExpiration = true;
    opt.Cookie.Name = "SparkNest";
});

//Helpers 
builder.Services.AddSingleton<FileStockHelper>();

//Fluent Validation
builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateVMValidator>(); // register validators

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
//app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
