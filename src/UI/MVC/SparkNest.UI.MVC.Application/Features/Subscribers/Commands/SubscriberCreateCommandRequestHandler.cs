using MediatR;
using SparkNest.UI.MVC.Application.Abstractions;

namespace SparkNest.UI.MVC.Application.Features.Subscribers.Commands
{
    public class SubscriberCreateCommandRequestHandler : IRequestHandler<SubscriberCreateCommandRequest, bool>
    {
        IBlogService _blogService;

        public SubscriberCreateCommandRequestHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<bool> Handle(SubscriberCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _blogService.SubscibeAsync(request.Email);
            return result;
        }
    }
}
