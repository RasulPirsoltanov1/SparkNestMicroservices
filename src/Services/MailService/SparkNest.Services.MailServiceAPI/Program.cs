
using MassTransit;
using SparkNest.Services.MailServiceAPI.Consumers;
using SparkNest.Services.MailServiceAPI.Interfaces;
using SparkNest.Services.MailServiceAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMailService, MailService>();


builder.Services.AddMassTransit(options =>
{
    options.AddConsumer<OrderStatusChangedEventConsumer>();
    options.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration["RabbitMQUrl"], cfg =>
        {
            cfg.Password("guest");
            cfg.Username("guest");
        

        });
        config.ReceiveEndpoint("order-status-changed-event", e =>
        {
            e.ConfigureConsumer<OrderStatusChangedEventConsumer>(context);
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
