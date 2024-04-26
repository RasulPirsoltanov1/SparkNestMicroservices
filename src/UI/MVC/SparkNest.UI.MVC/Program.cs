using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SparkNest.Common.Base.Services;
using SparkNest.UI.MVC.Extensions;
using SparkNest.UI.MVC.Handlers;
using SparkNest.UI.MVC.Helpers;
using SparkNest.UI.MVC.Hubs;
using SparkNest.UI.MVC.Infrastructure;
using SparkNest.UI.MVC.Models;
using SparkNest.UI.MVC.Services.Concretes;
using SparkNest.UI.MVC.Services.Interfaces;
using SparkNest.UI.MVC.Validators;
using SparkNest.UI.MVC.Application.Features.Messages.Queries.GetAllMessages;
using SparkNest.UI.MVC.Application.Abstractions;
using SparkNest.UI.MVC.Infrastructure.Concretes;
using SparkNest.UI.MVC.Application.Concretes;

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


builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IBlogService, BlogService>();


//MVC infrastructure Layer Registration
builder.Services.AddMvcInfrastructure();

//Http services 
builder.Services.AddHttpClientServices();

//Cookie Authentication Configurations
builder.Services.AddAuthentication().AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Auth/SignIn";
    opt.AccessDeniedPath = "/Auth/SignIn";
    opt.ExpireTimeSpan = TimeSpan.FromHours(7);
    opt.SlidingExpiration = true;
    opt.Cookie.Name = "SparkNest";

});

//Helpers 
builder.Services.AddSingleton<FileStockHelper>();

//Fluent Validation
builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateVMValidator>(); // register validators


builder.Services.AddSignalR();

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblyContaining<GetAllMessagesQueryRequestHandler>();
});




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

app.MapHub<ChatHub>("/chathub");

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=HomePage}/{action=Index}/{id?}");




app.Run();
