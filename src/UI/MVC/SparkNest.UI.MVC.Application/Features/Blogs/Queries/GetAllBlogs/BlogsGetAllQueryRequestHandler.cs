using MediatR;
using SparkNest.UI.MVC.Application.Abstractions;
using SparkNest.UI.MVC.Application.DTOs.Blog;

namespace SparkNest.UI.MVC.Application.Features.Blogs.Queries.GetAll
{
    public class BlogsGetAllQueryRequestHandler : IRequestHandler<BlogsGetAllQueryRequest, List<BlogDTO>>
    {
        IBlogService _blogService;

        public BlogsGetAllQueryRequestHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<List<BlogDTO>> Handle(BlogsGetAllQueryRequest request, CancellationToken cancellationToken)
        {
            var blogs = await _blogService.GetAllBlogs();
            return blogs;
        }
    }
}
