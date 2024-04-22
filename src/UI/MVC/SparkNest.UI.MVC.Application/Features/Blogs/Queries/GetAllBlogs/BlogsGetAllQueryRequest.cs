using MediatR;
using SparkNest.UI.MVC.Application.DTOs.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Application.Features.Blogs.Queries.GetAll
{
    public class BlogsGetAllQueryRequest : IRequest<List<BlogDTO>>
    {
    }
}
