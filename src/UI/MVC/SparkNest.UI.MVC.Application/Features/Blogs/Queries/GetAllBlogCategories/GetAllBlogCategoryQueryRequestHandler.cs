using MediatR;
using SparkNest.UI.MVC.Application.Abstractions;
using SparkNest.UI.MVC.Application.DTOs.Blog;

namespace SparkNest.UI.MVC.Application.Features.Blogs.Queries.GetAllBlogCategories
{
    public class GetAllBlogCategoryQueryRequestHandler : IRequestHandler<GetAllBlogCategoryQueryRequest, List<BlogCategoryDTO>>
    {
        IBlogService _blogService;

        public GetAllBlogCategoryQueryRequestHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<List<BlogCategoryDTO>> Handle(GetAllBlogCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var blogCategories = await _blogService.GetAllCategories();
            return blogCategories;
        }
    }
}
