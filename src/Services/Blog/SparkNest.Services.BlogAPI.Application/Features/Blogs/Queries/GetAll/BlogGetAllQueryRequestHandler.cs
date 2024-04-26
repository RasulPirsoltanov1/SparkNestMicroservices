using MediatR;
using SparkNest.Common.DTOs;
using SparkNest.Services.BlogAPI.Application.DTOs;
using SparkNest.Services.BlogAPI.Application.Interfaces;

namespace SparkNest.Services.BlogAPI.Application.Features.Blogs.Queries.GetAll
{
    public class BlogGetAllQueryRequestHandler : IRequestHandler<BlogGetAllQueryRequest, Response<List<BlogDTO>>>
    {
        IBlogService _blogService;

        public BlogGetAllQueryRequestHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<Response<List<BlogDTO>>> Handle(BlogGetAllQueryRequest request, CancellationToken cancellationToken)
        {
            var blogs = await _blogService.GetAllAsync();
            return blogs;
        }
    }
}
