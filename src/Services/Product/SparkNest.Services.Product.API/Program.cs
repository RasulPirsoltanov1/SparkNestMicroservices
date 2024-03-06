using Microsoft.Extensions.DependencyInjection;
using SparkNest.Services.ProductAPI.Services;
using SparkNest.Services.ProductAPI.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());



builder.Services.AddSingleton<DatabaseSettings>(x =>
{
    return builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();
});
builder.Services.AddSingleton<IDatabaseSettings>(x =>
{
    return x.GetRequiredService<DatabaseSettings>();
});

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();


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
