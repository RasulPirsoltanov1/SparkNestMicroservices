using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SparkNest.Common.DTOs;
using SparkNest.Services.CommentAPI.Application.Abstractions;
using SparkNest.Services.CommentAPI.Domain.Entites;
using SparkNest.Services.CommentAPI.Infrastructure.Data;

namespace SparkNest.Services.CommentAPI.Application.Features.Comments.Queries.GetAllComments
{
    public class GetAllCommentsRequestHandler : IRequestHandler<GetAllCommentsRequest, Response<List<GetAllCommentsResponse>>>
    {
        CommentDbContext dbContext;
        Services.CommentAPI.Application.Abstractions.IRedisService<List<Comment>> _redisService;
        public GetAllCommentsRequestHandler(CommentDbContext dbContext, IRedisService<List<Comment>> redisService)
        {
            this.dbContext = dbContext;
            _redisService = redisService;
        }

        public async Task<Response<List<GetAllCommentsResponse>>> Handle(GetAllCommentsRequest request, CancellationToken cancellationToken)
        {

            var redisComments = await _redisService.GetStringAsync(nameof(Comment));
            if (redisComments != null)
            {
                List<GetAllCommentsResponse> getAllCommentsRedisResponses = redisComments.Select(x => new GetAllCommentsResponse
                {
                    UserId = x.UserId,
                    Content = x.Content,
                    Id = x.Id,
                    ProductId = x.ProductId,
                    UserName = x.UserName,
                    PhotoUrl = x.PhotoUrl,
                    CreateDate = x.CreateDate,
                }).ToList();
                return Response<List<GetAllCommentsResponse>>.Success(getAllCommentsRedisResponses, 200);
            }

            List<Comment> comments = await dbContext.Comments.ToListAsync();
            await _redisService.SaveStringAsync(nameof(Comment), JsonConvert.SerializeObject(comments));
            List<GetAllCommentsResponse> getAllCommentsResponses = comments.Select(x => new GetAllCommentsResponse
            {
                Id = x.Id,
                UserId = x.UserId,
                Content = x.Content,
                ProductId = x.ProductId,
                UserName = x.UserName,
                PhotoUrl = x.PhotoUrl,
                CreateDate = x.CreateDate,
            }).ToList();
            return Response<List<GetAllCommentsResponse>>.Success(getAllCommentsResponses, 200);
        }
    }
}
