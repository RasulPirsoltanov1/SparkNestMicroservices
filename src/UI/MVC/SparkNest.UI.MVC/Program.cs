using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SparkNest.UI.MVC.Handlers;
using SparkNest.UI.MVC.Models;
using SparkNest.UI.MVC.Services.Concretes;
using SparkNest.UI.MVC.Services.Interfaces;

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


builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
var serviceApiASettings = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
builder.Services.AddHttpClient<IUserService, UserService>(opt =>
{
    opt.BaseAddress = new Uri(serviceApiASettings.IdentityBaseUri);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthentication().AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Auth/SignIn";
    opt.ExpireTimeSpan = TimeSpan.FromMicroseconds(10);
    opt.SlidingExpiration = true;
    opt.Cookie.Name = "SparkNest";
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
