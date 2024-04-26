using MediatR;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SparkNest.Services.BlogAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparkNest.Common.DTOs;

namespace SparkNest.Services.BlogAPI.Application.Features.Blogs.Commands.Create
{
    public class BlogCreateRequest : IRequest<Response<bool>>
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int? Views { get; set; }
        public List<string>? ViewedUser { get; set; } = new List<string>();
        public string? CategoryId { get; set; }
        public string? PhotoUrl { get; set; }
        public virtual DateTime? CreateDate { get; set; }
    }
}
