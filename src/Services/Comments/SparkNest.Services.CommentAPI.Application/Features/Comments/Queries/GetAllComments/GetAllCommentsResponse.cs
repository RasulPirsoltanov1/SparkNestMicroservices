using MediatR;

namespace SparkNest.Services.CommentAPI.Application.Features.Comments.Queries.GetAllComments
{
    public class GetAllCommentsResponse : IRequest
    {
        public int? Id { get; set; }
        public string? ProductId { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Content { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
