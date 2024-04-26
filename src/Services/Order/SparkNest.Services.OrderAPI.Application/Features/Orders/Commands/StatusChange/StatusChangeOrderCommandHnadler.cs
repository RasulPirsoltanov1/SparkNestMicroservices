using MassTransit;
using MediatR;
using SparkNest.Common.DTOs;
using SparkNest.MessagesAndEvents.Base.Events;
using SparkNest.Services.OrderAPI.Domain.OrderAggregate;
using SparkNest.Services.OrderAPI.Infrastructure.Data;

namespace SparkNest.Services.OrderAPI.Application.Features.Orders.Commands.StatusChange
{
    public class StatusChangeOrderCommandHnadler : IRequestHandler<StatusChangeOrderCommand, Common.DTOs.Response<bool>>
    {
        OrderDbContext _dbContext;
        public readonly IPublishEndpoint _publishEndpoint;
        public StatusChangeOrderCommandHnadler(OrderDbContext dbContext, IPublishEndpoint publishEndpoint)
        {
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Common.DTOs.Response<bool>> Handle(StatusChangeOrderCommand request, CancellationToken cancellationToken)
        {
            Order? order = await _dbContext.Orders.FindAsync(request.OrderId);
            if (order is null)
            {
                return Common.DTOs.Response<bool>.Fail("order not found!", 404);
            }
            order.Status = request.Status;

            await _dbContext.SaveChangesAsync();
            var eventMessage = new OrderStatusChangedEvent
            {
                Address = order.Address,
                Email = order.Email,
                Name = order.UserName,
            };
            switch (order.Status)
            {
                case 0:
                    eventMessage.Status = "Müraciətiniz yoxlanilir.";
                    break;
                case 1:
                    eventMessage.Status = "Müraciətiniz tesdiqlədi və kargo firmasina tehvil verildi.";
                    break;
                case 2:
                    eventMessage.Status = "Sifaris ugurla catdirildi.";
                    break;
                default:
                    break;
            }
            await _publishEndpoint.Publish(eventMessage);
            return Common.DTOs.Response<bool>.Success(true, 200);
        }
    }
}
