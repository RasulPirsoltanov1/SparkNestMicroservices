using MediatR;
using SparkNest.Common.DTOs;
using SparkNest.Services.BlogAPI.Application.Interfaces;

namespace SparkNest.Services.BlogAPI.Application.Features.Blogs.Commands.Delete
{
    public class BlogDeleteCommandRequestHandler : IRequestHandler<BlogDeleteCommandRequest, Response<bool>>
    {
        IBlogService _blogService;

        public BlogDeleteCommandRequestHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<Response<bool>> Handle(BlogDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _blogService.DeleteAsync(request.BlogId);
            return result;
        }
    }
}
