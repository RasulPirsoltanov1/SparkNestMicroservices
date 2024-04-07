using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using SparkNest.Common.Base.Messages;
using SparkNest.Common.Base.Services;
using SparkNest.Services.OrderAPI.Application.Consumers;
using SparkNest.Services.OrderAPI.Application.EventConsumers;
using SparkNest.Services.OrderAPI.Application.Features.Orders.Queries.Get;
using SparkNest.Services.OrderAPI.Infrastructure.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var authenticationBuilder = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(authenticationBuilder));
});
builder.Services.AddMediatR(Assembly.GetAssembly(typeof(GetOrderByUserIdQueryHandler)));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrderDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), opt =>
    {
        //opt.MigrationsAssembly("SparkNest.Services.OrderAPI.Infrastructure");
    });
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServiceUrl"];
    options.Audience = "resource_order";
    options.RequireHttpsMetadata = false;
});


builder.Services.AddMassTransit(x =>
{
    // Default Port : 5672
    x.AddConsumer<CreateOrderMessageCommandConsumer>();
    x.AddConsumer<ProductNameChangedEventConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint("create-order-service", e =>
        {
            e.ConfigureConsumer<CreateOrderMessageCommandConsumer>(context);
        });

        cfg.ReceiveEndpoint("create-order-service_error", e =>
        {
            e.ConfigureConsumer<CreateOrderMessageCommandConsumer>(context);
        });
        cfg.ReceiveEndpoint("product-name-changed-service", e =>
        {
            e.ConfigureConsumer<ProductNameChangedEventConsumer>(context);
        });
        cfg.ReceiveEndpoint("product-name-changed-service_skipped", e =>
        {
            e.ConfigureConsumer<ProductNameChangedEventConsumer>(context);
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
