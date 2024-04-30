using MediatR;
using Microsoft.EntityFrameworkCore;
using SparkNest.Common.DTOs;
using SparkNest.Services.OrderAPI.Application.Abstractions;
using SparkNest.Services.OrderAPI.Application.DTOs;
using SparkNest.Services.OrderAPI.Application.Features.Orders.Queries.GetAll;
using SparkNest.Services.OrderAPI.Application.Mapping;
using SparkNest.Services.OrderAPI.Domain.OrderAggregate;
using SparkNest.Services.OrderAPI.Infrastructure.Data;
using System.Text.Json;

namespace SparkNest.Services.OrderAPI.Application.Features.Orders.Queries.GetAll
{
    public class GetAllOrdersQueryQueryHandler : IRequestHandler<GetAllOrdersQuery, Response<List<OrderDTO>>>
    {
        OrderDbContext _orderDbContext;
        IRedisService<List<OrderDTO>> _redisService;

        public GetAllOrdersQueryQueryHandler(OrderDbContext orderDbContext, IRedisService<List<OrderDTO>> redisService)
        {
            _orderDbContext = orderDbContext;
            _redisService = redisService;
        }

        public async Task<Response<List<OrderDTO>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var redisOrders = await _redisService.GetStringAsync(nameof(Order));

            if (redisOrders != null)
            {
                var orderRedisDtos = ObjectMapping.Mapper.Map<List<OrderDTO>>(redisOrders);
                return Response<List<OrderDTO>>.Success(orderRedisDtos, 200);
            }
            var orders = await _orderDbContext.Orders.Include(x => x.OrderItems).ToListAsync();
            if (orders == null || orders.Count <= 0)
            {
                return Response<List<OrderDTO>>.Success(new List<OrderDTO>(), 200);
            }
            var orderDtos = ObjectMapping.Mapper.Map<List<OrderDTO>>(orders);
            await _redisService.SaveStringAsync(nameof(Order), JsonSerializer.Serialize(orderDtos));
            return Response<List<OrderDTO>>.Success(orderDtos, 200);
        }
    }
}
