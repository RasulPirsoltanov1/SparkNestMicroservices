using MediatR;
using SparkNest.UI.MVC.Application.Abstractions;
using SparkNest.UI.MVC.Application.DTOs.Blog;

namespace SparkNest.UI.MVC.Application.Features.Blogs.Queries.GetById
{
    public class BlogGetByIdQueryRequestHandler : IRequestHandler<BlogGetByIdQueryRequest, BlogDTO>
    {
        IBlogService _blogService;

        public BlogGetByIdQueryRequestHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<BlogDTO> Handle(BlogGetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var blogs = await _blogService.GetByBlogId(request.BlogId);
            return blogs;
        }
    }
}
