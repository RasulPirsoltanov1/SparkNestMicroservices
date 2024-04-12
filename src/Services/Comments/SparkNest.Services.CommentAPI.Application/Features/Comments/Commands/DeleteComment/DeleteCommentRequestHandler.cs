using MediatR;
using SparkNest.Common.DTOs;
using SparkNest.Services.CommentAPI.Infrastructure.Data;

namespace SparkNest.Services.CommentAPI.Application.Features.Comments.Commands.CreateComment
{
    public class DeleteCommentRequestHandler : IRequestHandler<DeleteCommentRequest, Response<bool>>
    {
        CommentDbContext _dbContext;

        public DeleteCommentRequestHandler(CommentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<bool>> Handle(DeleteCommentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var comment = await _dbContext.Comments.FindAsync(request.Id);
                _dbContext.Comments.Remove(comment);
                await _dbContext.SaveChangesAsync();
                return Response<bool>.Success(true, 200);
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message,500);
            }
        }
    }
}
