using MediatR;
using SparkNest.UI.MVC.Application.Abstractions;

namespace SparkNest.UI.MVC.Application.Features.Blogs.Commands.DeleteBlog
{
    public class BlogDeleteCommandRequestHandler : IRequestHandler<BlogDeleteCommandRequest, bool>
    {
        IBlogService _blogService;

        public BlogDeleteCommandRequestHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<bool> Handle(BlogDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _blogService.DeleteAsync(request.BlogId);
            return result;
        }
    }
}
