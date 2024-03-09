using Microsoft.Extensions.DependencyInjection;
using SparkNest.Services.BasketAPI.Services.Concrete;
using SparkNest.Services.BasketAPI.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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
