using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Application.Features.Blogs.Commands.Update
{
    public class BlogUpdateCommandRequest:IRequest<bool>
    {
        public virtual string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CategoryId { get; set; }
        public string? PhotoUrl { get; set; }
        public string? PhotoFileStockUrl { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
