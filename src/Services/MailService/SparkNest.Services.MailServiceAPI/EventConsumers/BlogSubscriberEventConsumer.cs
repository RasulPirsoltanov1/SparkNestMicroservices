using MassTransit;
using SparkNest.MessagesAndEvents.Base.News;
using SparkNest.Services.MailServiceAPI.Interfaces;
using SparkNest.Services.MailServiceAPI.Services;

namespace SparkNest.Services.MailServiceAPI.EventConsumers
{
    public class BlogSubscriberEventConsumer : IConsumer<BlogsSubscribeEvent>
    {
        public static List<string> Emails { get; set; } = new List<string>();

        ISubscriberService _subscriberService;
        IMailService _mailService;

        public BlogSubscriberEventConsumer(ISubscriberService subscriberService, IMailService mailService)
        {
            _subscriberService = subscriberService;
            _mailService = mailService;
        }

        public async Task Consume(ConsumeContext<BlogsSubscribeEvent> context)
        {
            await _subscriberService.AddAsync(new Models.Subscriber
            {
                Email = context.Message.Email,
                CreateDate = DateTime.Now,
            });
            _mailService.SendMail(context.Message.Email,"SparkNest News" , "\"Salam artiq SparkNest yazılarından anında xəbərdar olacaqsiniz\"", "http://localhost:2007/Blogs/Index");
        }
    }
}
