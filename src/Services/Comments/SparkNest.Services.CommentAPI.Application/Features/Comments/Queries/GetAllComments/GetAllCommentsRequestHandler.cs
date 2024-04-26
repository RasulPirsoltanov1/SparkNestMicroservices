using MediatR;
using Microsoft.EntityFrameworkCore;
using SparkNest.Common.DTOs;
using SparkNest.Services.CommentAPI.Domain.Entites;
using SparkNest.Services.CommentAPI.Infrastructure.Data;

namespace SparkNest.Services.CommentAPI.Application.Features.Comments.Queries.GetAllComments
{
    public class GetAllCommentsRequestHandler : IRequestHandler<GetAllCommentsRequest, Response<List<GetAllCommentsResponse>>>
    {
        CommentDbContext dbContext;

        public GetAllCommentsRequestHandler(CommentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response<List<GetAllCommentsResponse>>> Handle(GetAllCommentsRequest request, CancellationToken cancellationToken)
        {
            List<Comment> comments = await dbContext.Comments.ToListAsync();

            List<GetAllCommentsResponse> getAllCommentsResponses = comments.Select(x => new GetAllCommentsResponse
            {
                UserId = x.UserId,
                Content = x.Content,
                Id = x.Id,
                ProductId = x.ProductId,
                UserName = x.UserName,
                PhotoUrl = x.PhotoUrl,
                CreateDate = x.CreateDate,
            }).ToList();
            return Response<List<GetAllCommentsResponse>>.Success(getAllCommentsResponses, 200);
        }
    }
}
