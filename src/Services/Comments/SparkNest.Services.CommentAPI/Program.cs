using Microsoft.EntityFrameworkCore;
using SparkNest.Services.CommentAPI.Application.Abstractions;
using SparkNest.Services.CommentAPI.Application.Concretes;
using SparkNest.Services.CommentAPI.Application.Features.Comments.Queries.GetAllComments;
using SparkNest.Services.CommentAPI.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CommentDbContext>(op =>
{
    op.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});


builder.Services.AddStackExchangeRedisCache(option =>
{
    option.Configuration = "localhost:6379";
    option.InstanceName = "RedisComment";
});

builder.Services.AddSingleton(typeof(SparkNest.Services.CommentAPI.Application.Abstractions.IRedisService<>), typeof(RedisService<>));
builder.Services.AddMediatR(opt =>
{
    opt.RegisterServicesFromAssemblyContaining<GetAllCommentsRequestHandler>();
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
