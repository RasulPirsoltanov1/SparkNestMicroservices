using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using SparkNest.Common.Base.Services;
using SparkNest.Services.DiscountAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var authenticationBuilder = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(authenticationBuilder));
});

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityServiceUrl"];
    opt.RequireHttpsMetadata = false;
    opt.Audience = "resource_discount";
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();


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
