using MediatR;
using SparkNest.UI.MVC.Application.Abstractions;

namespace SparkNest.UI.MVC.Application.Features.BlogCategory.Commands.Delete
{
    public class BlogCategoryDeleteCommandRequestHnadler : IRequestHandler<BlogCategoryDeleteCommandRequest, bool>
    {
        IBlogService _blogService;

        public BlogCategoryDeleteCommandRequestHnadler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<bool> Handle(BlogCategoryDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _blogService.DeleteCategoryAsync(request.CategoryId);
            return result;
        }
    }
}
