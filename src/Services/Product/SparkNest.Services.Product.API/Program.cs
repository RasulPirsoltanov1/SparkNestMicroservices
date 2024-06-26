using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using SparkNest.Common.Base.Services;
using SparkNest.Services.ProductAPI.Application.Abstrctions;
using SparkNest.Services.ProductAPI.Infrastructure.Concretes;
using SparkNest.Services.ProductAPI.Services;
using SparkNest.Services.ProductAPI.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(opt =>
{
    //opt.Filters.Add(new AuthorizeFilter());
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddAuthentication(cfg =>
{
    cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServiceUrl"];
    options.Audience = "resource_product";
    options.RequireHttpsMetadata = false;
});


builder.Services.AddSingleton<DatabaseSettings>(x =>
{
    return builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();
});
builder.Services.AddSingleton<IDatabaseSettings>(x =>
{
    return x.GetRequiredService<DatabaseSettings>();
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddScoped(typeof(IRedisService<>), typeof(RedisService<>));





builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });
    });
});

builder.Services.AddMassTransitHostedService();





builder.Services.AddStackExchangeRedisCache(option =>
{
    option.Configuration = "localhost:6379";
    option.InstanceName = "RedisProduct";
});




var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var categoryService = scope.ServiceProvider.GetRequiredService<ICategoryService>();
    if (!categoryService.GetAllAsync().Result.Data.Any())
    {
        for (int i = 0; i < 10; i++)
        {
            await categoryService.CreateAsync(new SparkNest.Services.ProductAPI.DTOs.CategoryDTO { Name = "Test " + i });
        }
    }
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
