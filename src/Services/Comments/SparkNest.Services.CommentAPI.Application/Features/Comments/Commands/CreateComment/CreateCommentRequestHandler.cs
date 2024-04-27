using MediatR;
using Newtonsoft.Json;
using SparkNest.Common.DTOs;
using SparkNest.Services.CommentAPI.Application.Abstractions;
using SparkNest.Services.CommentAPI.Domain.Entites;
using SparkNest.Services.CommentAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.CommentAPI.Application.Features.Comments.Commands.CreateComment
{
    public class CreateCommentRequestHandler : IRequestHandler<CreateCommentRequest, Response<bool>>
    {
        CommentDbContext dbContext;
        IRedisService<List<Comment>> _redisService;

        public CreateCommentRequestHandler(CommentDbContext dbContext, IRedisService<List<Comment>> redisService)
        {
            this.dbContext = dbContext;
            _redisService = redisService;
        }

        public async Task<Response<bool>> Handle(CreateCommentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var comment = new Comment
                {
                    UserId = request.UserId,
                    ProductId = request.ProductId,
                    Content = request.Content,
                    PhotoUrl = request.PhotoUrl,
                    UserName = request.UserName,
                    CreateDate=DateTime.UtcNow,
                };
                await dbContext.Comments.AddAsync(comment);
                await dbContext.SaveChangesAsync();
                await _redisService.SaveStringAsync(nameof(Comment), JsonConvert.SerializeObject(dbContext.Comments.ToList()));
                return Response<bool>.Success(true, 200);
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message,500);
            }
        }
    }
}
