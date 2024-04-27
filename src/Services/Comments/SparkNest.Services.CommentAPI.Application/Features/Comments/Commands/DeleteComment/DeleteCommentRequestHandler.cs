using MediatR;
using Newtonsoft.Json;
using SparkNest.Common.DTOs;
using SparkNest.Services.CommentAPI.Application.Abstractions;
using SparkNest.Services.CommentAPI.Domain.Entites;
using SparkNest.Services.CommentAPI.Infrastructure.Data;

namespace SparkNest.Services.CommentAPI.Application.Features.Comments.Commands.CreateComment
{
    public class DeleteCommentRequestHandler : IRequestHandler<DeleteCommentRequest, Response<bool>>
    {
        CommentDbContext _dbContext;
        IRedisService<List<Comment>> _redisService;
        public DeleteCommentRequestHandler(CommentDbContext dbContext, IRedisService<List<Comment>> redisService)
        {
            _dbContext = dbContext;
            _redisService = redisService;
        }

        public async Task<Response<bool>> Handle(DeleteCommentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var comment = await _dbContext.Comments.FindAsync(request.Id);
                _dbContext.Comments.Remove(comment);
                await _dbContext.SaveChangesAsync();
                await _redisService.SaveStringAsync(nameof(Comment), JsonConvert.SerializeObject(_dbContext.Comments.ToList()));
                return Response<bool>.Success(true, 200);
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message,500);
            }
        }
    }
}
