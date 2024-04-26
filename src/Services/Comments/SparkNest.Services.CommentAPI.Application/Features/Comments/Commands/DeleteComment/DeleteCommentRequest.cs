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
    public class DeleteCommentRequest:IRequest<Response<bool>>
    {
        public int Id{ get; set; }
    }
}
