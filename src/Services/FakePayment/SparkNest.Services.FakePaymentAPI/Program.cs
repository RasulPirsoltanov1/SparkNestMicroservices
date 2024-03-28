using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using SparkNest.Common.Base.Messages;
using SparkNest.Services.FakePaymentAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var authorizationBuilder = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter(authorizationBuilder));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddMassTransit(config =>
//{
//    config.UsingRabbitMq((context, cfg) =>
//    {
//        cfg.ReceiveEndpoint("createorderservice", ep =>
//        {
//            ep.ConfigureConsumer<CreateOrderMessageCommand>(context); // Configure consumer
//        });
//        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
//        {
//            host.Password("guest");
//            host.Username("guest");
//        });
//    });
//});

//builder.Services.AddMassTransit(busConfigurator =>
//{
//    busConfigurator.SetKebabCaseEndpointNameFormatter();
//    busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
//    {
//        busFactoryConfigurator.Host(builder.Configuration["RabbitMQUrl"], "/", hostConfigurator => {

//            hostConfigurator.Password("guest");
//            hostConfigurator.Username("guest");
//        });
//    });
//});
//builder.Services.AddScoped<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());


//builder.Services.AddMassTransitHostedService(); //deprecated




builder.Services.AddMassTransit(x =>
{
    // Default Port : 5672
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







builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServiceUrl"];
    options.RequireHttpsMetadata = false;
    options.Audience = "resource_fakepayment";
});


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
