using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Application.Features.Blogs.Commands.DeleteBlog
{
    public class BlogDeleteCommandRequest:IRequest<bool>
    {
        [Required]
        public string BlogId { get; set; }
    }
}
