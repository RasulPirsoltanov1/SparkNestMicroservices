using MediatR;
using SparkNest.Common.DTOs;
using SparkNest.Services.BlogAPI.Application.Interfaces;
using SparkNest.Services.BlogAPI.Domain.Entities;

namespace SparkNest.Services.BlogAPI.Application.Features.Subscribers.Queries.GetAll
{
    public class SubscriberGetAllQueryRequestHandler : IRequestHandler<SubscriberGetAllQueryRequest, Response<List<Subscriber>>>
    {
        ISubscriberService _subscriberService;

        public SubscriberGetAllQueryRequestHandler(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        public async Task<Response<List<Subscriber>>> Handle(SubscriberGetAllQueryRequest request, CancellationToken cancellationToken)
        {
            var subscribers = await _subscriberService.GetAllAsync();
            return subscribers;
        }
    }
}
