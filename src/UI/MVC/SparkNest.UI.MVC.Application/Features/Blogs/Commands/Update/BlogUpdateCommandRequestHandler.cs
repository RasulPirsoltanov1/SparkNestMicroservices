using MediatR;
using SparkNest.UI.MVC.Application.Abstractions;

namespace SparkNest.UI.MVC.Application.Features.Blogs.Commands.Update
{
    public class BlogUpdateCommandRequestHandler : IRequestHandler<BlogUpdateCommandRequest, bool>
    {
        IBlogService _blogService;

        public BlogUpdateCommandRequestHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<bool> Handle(BlogUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _blogService.UpdateBlogAsync(new DTOs.Blog.BlogUpdateDTO
            {
                CategoryId = request.CategoryId,
                Content = request.Content,
                Id = request.Id,
                Photo = request.Photo,
                Title = request.Title,
            });
            return result;
        }
    }
}
