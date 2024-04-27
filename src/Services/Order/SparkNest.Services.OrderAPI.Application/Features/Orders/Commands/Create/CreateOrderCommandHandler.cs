using MediatR;
using Microsoft.EntityFrameworkCore;
using SparkNest.Common.DTOs;
using SparkNest.Services.OrderAPI.Application.Abstractions;
using SparkNest.Services.OrderAPI.Application.DTOs;
using SparkNest.Services.OrderAPI.Application.Mapping;
using SparkNest.Services.OrderAPI.Domain.OrderAggregate;
using SparkNest.Services.OrderAPI.Infrastructure.Data;
using System.Text.Json;

namespace SparkNest.Services.OrderAPI.Application.Features.Orders.Commands.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDTO>>
    {
        OrderDbContext _dbContext;
        IRedisService<List<Order>> _redisService;
        public CreateOrderCommandHandler(OrderDbContext dbContext, IRedisService<List<Order>> redisService)
        {
            _dbContext = dbContext;
            _redisService = redisService;
        }

        public async Task<Response<CreatedOrderDTO>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = new Order(request.BuyerId, request.UserName, request.Email, new Address(request.Address.Province, request.Address.Street, request.Address.District, request.Address.Line, request.Address.ZipCode));
            request.OrderItems.ForEach(x =>
            {
                order.AddOrderItem(x.ProductId, x.ProductName, x.ProductUrl, x.Price, x.Quantity);
            });
            order.UserName = request.UserName;
            order.Email = request.Email;
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            var orders = await _dbContext.Orders.Include(x => x.OrderItems).ToListAsync();
            if (orders == null || orders.Count <= 0)
            {
                await _redisService.SaveStringAsync(nameof(Order), JsonSerializer.Serialize(orders));
            }
            return Response<CreatedOrderDTO>.Success(new CreatedOrderDTO { OrderId = order.Id }, 200);
        }
    }
}
