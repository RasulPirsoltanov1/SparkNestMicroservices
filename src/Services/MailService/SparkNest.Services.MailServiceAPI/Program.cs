
using MassTransit;
using MongoDB.Driver;
using SparkNest.MessagesAndEvents.Base.News;
using SparkNest.Services.MailServiceAPI.Consumers;
using SparkNest.Services.MailServiceAPI.EventConsumers;
using SparkNest.Services.MailServiceAPI.Interfaces;
using SparkNest.Services.MailServiceAPI.Services;
using SparkNest.Services.MailServiceAPI.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMongoDatabase>(x =>
{
    var configs = builder?.Configuration?.GetSection("DatabaseSettings").Get<DatabaseSettings>();
    var mongoClient = new MongoClient(configs?.ConnectionString);
    var database = mongoClient.GetDatabase(configs?.DatabaseName);
    return database;
});






builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<ISubscriberService, SubscriberService>();








builder.Services.AddMassTransit(options =>
{
    options.AddConsumer<OrderStatusChangedEventConsumer>();
    options.AddConsumer<BlogSubscriberEventConsumer>();
    options.AddConsumer<SubscriberNotificationEventConsumer>();

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
        config.ReceiveEndpoint("blog-subscibe-event", e =>
        {
            e.ConfigureConsumer<BlogSubscriberEventConsumer>(context);
        });
        config.ReceiveEndpoint("subsciber-notification-event", e =>
        {
            e.ConfigureConsumer<SubscriberNotificationEventConsumer>(context);
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
