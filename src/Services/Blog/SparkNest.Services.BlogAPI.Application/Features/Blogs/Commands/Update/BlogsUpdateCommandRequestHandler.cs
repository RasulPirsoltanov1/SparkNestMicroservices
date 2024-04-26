using MediatR;
using SparkNest.Common.DTOs;
using SparkNest.Services.BlogAPI.Application.Interfaces;

namespace SparkNest.Services.BlogAPI.Application.Features.Blogs.Commands.Update
{
    public class BlogsUpdateCommandRequestHandler : IRequestHandler<BlogsUpdateCommandRequest, Response<bool>>
    {
        IBlogService _blogService;

        public BlogsUpdateCommandRequestHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<Response<bool>> Handle(BlogsUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _blogService.UpdateAsync(new DTOs.BlogUpdateDTO
                {
                    Id = request.Id,
                    CategoryId = request.CategoryId,
                    Content = request.Content,
                    PhotoUrl = request.PhotoUrl,
                    Title = request.Title,
                });
                return Response<bool>.Success(true, 200);
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message,400);
            }

        }
    }
}
