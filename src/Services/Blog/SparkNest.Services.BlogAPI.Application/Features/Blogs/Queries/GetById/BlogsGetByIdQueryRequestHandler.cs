using MediatR;
using SparkNest.Common.DTOs;
using SparkNest.Services.BlogAPI.Application.DTOs;
using SparkNest.Services.BlogAPI.Application.Interfaces;

namespace SparkNest.Services.BlogAPI.Application.Features.Blogs.Queries.GetById
{
    public class BlogsGetByIdQueryRequestHandler : IRequestHandler<BlogsGetByIdQueryRequest, Response<BlogDTO>>
    {
        IBlogService _blogService;

        public BlogsGetByIdQueryRequestHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<Response<BlogDTO>> Handle(BlogsGetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var blogs = await _blogService.GetByIdAsync(request.BlogId);
            return blogs;
        }
    }
}
