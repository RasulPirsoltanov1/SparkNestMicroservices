using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SparkNest.Services.BlogAPI.Application.Features.Blogs.Commands.Create;
using SparkNest.Services.BlogAPI.Application.Interfaces;
using SparkNest.Services.BlogAPI.Application.Settings;
using SparkNest.Services.BlogAPI.Infrastructure.Concretes;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DatabaseSettings>(x =>
{
    var configs = builder?.Configuration?.GetSection("DatabaseSettings").Get<DatabaseSettings>();
    return configs;
});


//mongo configs
builder.Services.AddScoped<IMongoDatabase>(x =>
{
    var configs = builder?.Configuration?.GetSection("DatabaseSettings").Get<DatabaseSettings>();
    var mongoClient = new MongoClient(configs?.ConnectionString);
    var database = mongoClient.GetDatabase(configs?.DatabaseName);
    return database;
});


//Mediator
builder.Services.AddMediatR(x =>
{
    x.RegisterServicesFromAssemblyContaining<BlogCreateRequestHandler>();
});

//services
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISubscriberService, SubscriberService>();



builder.Services.AddMassTransit(options =>
{
    options.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], opt =>
        {
            opt.Username("guest");
            opt.Password("guest");
        });
    });
});


builder.Services.AddMassTransitHostedService();




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
