using MediatR;
using Microsoft.EntityFrameworkCore;
using SparkNest.Common.DTOs;
using SparkNest.Services.OrderAPI.Application.DTOs;
using SparkNest.Services.OrderAPI.Application.Features.Orders.Queries.GetAll;
using SparkNest.Services.OrderAPI.Application.Mapping;
using SparkNest.Services.OrderAPI.Infrastructure.Data;

namespace SparkNest.Services.OrderAPI.Application.Features.Orders.Queries.GetAll
{
    public class GetAllOrdersQueryQueryHandler : IRequestHandler<GetAllOrdersQuery, Response<List<OrderDTO>>>
    {
        OrderDbContext _orderDbContext;

        public GetAllOrdersQueryQueryHandler(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<Response<List<OrderDTO>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderDbContext.Orders.Include(x => x.OrderItems).ToListAsync();
            if (orders == null || orders.Count <= 0)
            {
                return Response<List<OrderDTO>>.Success(new List<OrderDTO>(), 200);
            }
            var orderDtos = ObjectMapping.Mapper.Map<List<OrderDTO>>(orders);
            return Response<List<OrderDTO>>.Success(orderDtos, 200);
        }
    }
}
