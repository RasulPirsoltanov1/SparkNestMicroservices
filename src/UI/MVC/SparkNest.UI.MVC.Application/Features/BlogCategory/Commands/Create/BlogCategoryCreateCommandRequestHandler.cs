using MediatR;
using SparkNest.UI.MVC.Application.Abstractions;

namespace SparkNest.UI.MVC.Application.Features.BlogCategory.Commands.Create
{
    public class BlogCategoryCreateCommandRequestHandler : IRequestHandler<BlogCategoryCreateCommandRequest, bool>
    {
        IBlogService _blogService;

        public BlogCategoryCreateCommandRequestHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<bool> Handle(BlogCategoryCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _blogService.CreateCategoryAsync(new DTOs.Blog.BlogCategoryCreateDTO
            {
                Name = request.Name
            });
            return result;
        }
    }
}
