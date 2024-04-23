using MassTransit;
using MediatR;
using SparkNest.Common.DTOs;
using SparkNest.MessagesAndEvents.Base.News;
using SparkNest.Services.BlogAPI.Application.Interfaces;

namespace SparkNest.Services.BlogAPI.Application.Features.Subscribers.Commands.Create
{
    public class SubscriberCreateCommandRequestHandler : IRequestHandler<SubscriberCreateCommandRequest, Common.DTOs.Response<bool>>
    {
        ISubscriberService _subscriberService;
        IPublishEndpoint _publishEndpoint;

        public SubscriberCreateCommandRequestHandler(ISubscriberService subscriberService, IPublishEndpoint publishEndpoint)
        {
            _subscriberService = subscriberService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Common.DTOs.Response<bool>> Handle(SubscriberCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _subscriberService.CreateAsync(new Domain.Entities.Subscriber
            {
                Email = request.Email,
            });
            await _publishEndpoint.Publish<BlogsSubscribeEvent>(new BlogsSubscribeEvent
            {
                Email = request.Email
            });
            return response;
        }
    }
}
