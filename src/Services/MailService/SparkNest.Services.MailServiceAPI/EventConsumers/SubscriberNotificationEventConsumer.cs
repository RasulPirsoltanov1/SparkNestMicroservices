using MassTransit;
using SparkNest.MessagesAndEvents.Base.SubscriberNotification;
using SparkNest.Services.MailServiceAPI.Interfaces;

namespace SparkNest.Services.MailServiceAPI.EventConsumers
{
    public class SubscriberNotificationEventConsumer : IConsumer<SubscriberNotificationEvent>
    {
        ISubscriberService _subscriberService;
        IMailService _mailService;
        public SubscriberNotificationEventConsumer(ISubscriberService subscriberService, IMailService mailService)
        {
            this._subscriberService = subscriberService;
            _mailService = mailService;
        }

        public async Task Consume(ConsumeContext<SubscriberNotificationEvent> context)
        {
            var subscribers = await _subscriberService.GetAll();
            foreach (var sub in subscribers)
            {
                var content = context.Message.Content.Length < 1000 ? context.Message.Content : context.Message.Content.Substring(1000);
                _mailService.SendMail(sub.Email, context.Message.Title, "<hr/>" +context.Message.CategoryName + "<hr/>" +
                        $"{content}", "http://localhost:2007/Blogs/Index");
            }
        }
    }
}
