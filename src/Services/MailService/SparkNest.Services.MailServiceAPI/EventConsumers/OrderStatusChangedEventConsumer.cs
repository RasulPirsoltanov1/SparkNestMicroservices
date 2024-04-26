using MassTransit;
using SparkNest.MessagesAndEvents.Base.Events;
using SparkNest.Services.MailServiceAPI.Interfaces;

namespace SparkNest.Services.MailServiceAPI.Consumers
{
    public class OrderStatusChangedEventConsumer : IConsumer<OrderStatusChangedEvent>
    {
        IMailService _mailService;

        public OrderStatusChangedEventConsumer(IMailService mailService)
        {
            _mailService = mailService;
        }

        public Task Consume(ConsumeContext<OrderStatusChangedEvent> context)
        {
            _mailService.SendMail(context.Message.Email, "SparkNest Order Status Info", $"hormetli {context.Message.Name} hal hazirda {context.Message.Status}.", "http://localhost:2007/Order/Checkouthistory");
            return Task.CompletedTask;
        }
    }
}
