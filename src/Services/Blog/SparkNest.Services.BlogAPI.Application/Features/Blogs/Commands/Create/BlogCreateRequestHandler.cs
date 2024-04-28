using MediatR;
using SparkNest.Services.BlogAPI.Application.Interfaces;
using SparkNest.Common.DTOs;
using MassTransit;
using SparkNest.MessagesAndEvents.Base.SubscriberNotification;

namespace SparkNest.Services.BlogAPI.Application.Features.Blogs.Commands.Create
{
    public class BlogCreateRequestHandler : IRequestHandler<BlogCreateRequest, Common.DTOs.Response<bool>>
    {
        IBlogService _blogService;
        IPublishEndpoint _publishEndpoint;
        ICategoryService _categoryService;

        public BlogCreateRequestHandler(IBlogService blogService, IPublishEndpoint publishEndpoint, ICategoryService categoryService)
        {
            _blogService = blogService;
            _publishEndpoint = publishEndpoint;
            _categoryService = categoryService;
        }

        public async Task<Common.DTOs.Response<bool>> Handle(BlogCreateRequest request, CancellationToken cancellationToken)
        {
            var result = await _blogService.CreateAsync(new Domain.Entities.Blog
            {
                CategoryId = request.CategoryId,
                PhotoUrl = request.PhotoUrl,
                Content = request.Content,
                CreateDate = DateTime.Now,
                Title = request.Title,
            });
            var subEvent = new SubscriberNotificationEvent
            {
                CategoryName = (await _categoryService.GetByIdAsync(request.CategoryId)).Data.Name ?? "Default",
                Content = request.Content,
                Title = request.Title
            };
            await _publishEndpoint.Publish<SubscriberNotificationEvent>(subEvent);
            return result;
        }
    }
}
