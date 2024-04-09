using MediatR;
using SparkNest.Common.DTOs;
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

        public CreateCommentRequestHandler(CommentDbContext dbContext)
        {
            this.dbContext = dbContext;
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
                };
                await dbContext.Comments.AddAsync(comment);
                await dbContext.SaveChangesAsync();
                return Response<bool>.Success(true, 200);
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message,500);
            }
        }
    }
}
