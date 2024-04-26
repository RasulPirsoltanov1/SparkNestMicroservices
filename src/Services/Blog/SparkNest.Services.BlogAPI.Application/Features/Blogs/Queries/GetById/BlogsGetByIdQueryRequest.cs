using MediatR;
using SparkNest.Common.DTOs;
using SparkNest.Services.BlogAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.BlogAPI.Application.Features.Blogs.Queries.GetById
{
    public class BlogsGetByIdQueryRequest:IRequest<Response<BlogDTO>>
    {
        public string BlogId { get; set; }
    }
}
