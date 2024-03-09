using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using SparkNest.Common.Base.Services;
using SparkNest.Services.BasketAPI.Services.Abstract;
using SparkNest.Services.BasketAPI.Services.Concrete;
using SparkNest.Services.BasketAPI.Settings;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var authenticationBuilder = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(authenticationBuilder));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<RedisSettings>(builder?.Configuration?.GetSection("RedisSettings")?.Get<RedisSettings>());

builder.Services.AddSingleton<IRedisSettings>(provider =>
{
    var redisSettings = provider.GetRequiredService<RedisSettings>();
    return redisSettings;
});

builder.Services.AddSingleton<RedisService>(provider =>
{
    RedisSettings? redisSettings = provider.GetRequiredService<RedisSettings>();
    RedisService redisService  = new RedisService(redisSettings.Host,redisSettings.Port);
    redisService.Connect(); 
    return redisService;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddScoped<IBasketService, BasketService>();
//JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityServiceUrl"];
    opt.Audience = "resource_basket";
    opt.RequireHttpsMetadata = false;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
