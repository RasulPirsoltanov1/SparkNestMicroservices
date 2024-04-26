using MediatR;
using SparkNest.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.BlogAPI.Application.Features.Blogs.Commands.Update
{
    public class BlogsUpdateCommandRequest:IRequest<Response<bool>>
    {
        public virtual string Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? CategoryId { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
