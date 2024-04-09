using MediatR;
using SparkNest.Common.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.CommentAPI.Application.Features.Comments.Commands.CreateComment
{
    public class CreateCommentRequest:IRequest<Response<bool>>
    {
        [Required]
        public string ProductId { get; set; }
        [Required]
        public string? UserId { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Content { get; set; }
        public string? PhotoUrl{ get; set; }
    }
}
