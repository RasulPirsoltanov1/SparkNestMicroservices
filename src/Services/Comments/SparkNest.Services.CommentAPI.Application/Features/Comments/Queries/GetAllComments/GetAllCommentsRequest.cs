using MediatR;
using SparkNest.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.CommentAPI.Application.Features.Comments.Queries.GetAllComments
{
    public class GetAllCommentsRequest : IRequest<Response<List<GetAllCommentsResponse>>>
    {

    }
}
