using MediatR;
using SparkNest.Services.BlogAPI.Application.Interfaces;
using SparkNest.Common.DTOs;

namespace SparkNest.Services.BlogAPI.Application.Features.Blogs.Commands.Create
{
    public class BlogCreateRequestHandler : IRequestHandler<BlogCreateRequest, Response<bool>>
    {
        IBlogService _blogService;

        public BlogCreateRequestHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<Response<bool>> Handle(BlogCreateRequest request, CancellationToken cancellationToken)
        {
            var result = await _blogService.CreateAsync(new Domain.Entities.Blog
            {
                CategoryId = request.CategoryId,
                PhotoUrl = request.PhotoUrl,
                Content = request.Content,
                CreateDate = DateTime.Now,
                Title = request.Title,
            });
            return result;
        }
    }
}
