using MediatR;
using SparkNest.Common.DTOs;
using SparkNest.Services.OrderAPI.Infrastructure.Data;

namespace SparkNest.Services.OrderAPI.Application.Features.Orders.Commands.StatusChange
{
    public class StatusChangeOrderCommandHnadler : IRequestHandler<StatusChangeOrderCommand, Response<bool>>
    {
        OrderDbContext _dbContext;

        public StatusChangeOrderCommandHnadler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<bool>> Handle(StatusChangeOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FindAsync(request.OrderId);
            if (order is null)
            {
                return Response<bool>.Fail("order not found!", 404);
            }
            order.Status = request.Status;

            await _dbContext.SaveChangesAsync();
            return Response<bool>.Success(true,200);
        }
    }
}
