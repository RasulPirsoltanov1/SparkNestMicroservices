using Microsoft.EntityFrameworkCore;
using SparkNest.Services.CommentAPI.Application.Features.Comments.Queries.GetAllComments;
using SparkNest.Services.CommentAPI.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(opt =>
{
    opt.RegisterServicesFromAssemblyContaining<GetAllCommentsRequestHandler>();
});

builder.Services.AddDbContext<CommentDbContext>(op =>
{
    op.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
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
