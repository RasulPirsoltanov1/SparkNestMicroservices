using MediatR;
using SparkNest.Common.DTOs;
using SparkNest.Services.OrderAPI.Application.DTOs;
using SparkNest.Services.OrderAPI.Application.Mapping;
using SparkNest.Services.OrderAPI.Domain.OrderAggregate;
using SparkNest.Services.OrderAPI.Infrastructure.Data;

namespace SparkNest.Services.OrderAPI.Application.Features.Orders.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDTO>>
    {
        OrderDbContext _dbContext;

        public CreateOrderCommandHandler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<CreatedOrderDTO>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            Order order = new Order(request.BuyerId, new Address(request.Address.Province, request.Address.Street, request.Address.District, request.Address.Line, request.Address.ZipCode));
            request.OrderItems.ForEach(x =>
            {
                order.AddOrderItem(x.ProductId, x.ProductName, x.ProductUrl, x.Price);
            });
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return Response<CreatedOrderDTO>.Success(new CreatedOrderDTO { OrderId = order.Id }, 200);
        }
    }
}
