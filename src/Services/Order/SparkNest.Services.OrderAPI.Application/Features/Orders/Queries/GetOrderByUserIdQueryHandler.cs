using MediatR;
using Microsoft.EntityFrameworkCore;
using SparkNest.Common.DTOs;
using SparkNest.Services.OrderAPI.Application.DTOs;
using SparkNest.Services.OrderAPI.Application.Mapping;
using SparkNest.Services.OrderAPI.Infrastructure.Data;

namespace SparkNest.Services.OrderAPI.Application.Features.Orders.Queries
{
    public class GetOrderByUserIdQueryHandler : IRequestHandler<GetOrderByUserIdQuery, Response<List<OrderDTO>>>
    {
        OrderDbContext _orderDbContext;

        public GetOrderByUserIdQueryHandler(OrderDbContext orderDbContext)
        {
            this._orderDbContext = orderDbContext;
        }

        public async Task<Response<List<OrderDTO>>> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderDbContext.Orders.Include(x=>x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();
            if (orders == null || orders.Count <= 0)
            {
                return Response<List<OrderDTO>>.Success(new List<OrderDTO>(), 200);
            }
            var orderDtos = ObjectMapping.Mapper.Map<List<OrderDTO>>(orders);
            return Response<List<OrderDTO>>.Success(orderDtos, 200);
        }
    }
}
