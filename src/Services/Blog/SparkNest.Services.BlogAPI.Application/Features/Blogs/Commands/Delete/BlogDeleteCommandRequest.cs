using Amazon.Runtime.Internal;
using MediatR;
using SparkNest.Common.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.BlogAPI.Application.Features.Blogs.Commands.Delete
{
    public class BlogDeleteCommandRequest:IRequest<Response<bool>>
    {
        [Required]
        public string BlogId { get; set; }
    }
}
