using MediatR;
using SparkNest.UI.MVC.Application.DTOs.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Application.Features.Blogs.Queries.GetAllBlogCategories
{
    public class GetAllBlogCategoryQueryRequest:IRequest<List<BlogCategoryDTO>>
    {
        public virtual string Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? CategoryId { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
