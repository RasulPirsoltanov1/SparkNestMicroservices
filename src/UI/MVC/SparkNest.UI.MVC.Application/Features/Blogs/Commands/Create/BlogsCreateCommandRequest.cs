using MediatR;
using Microsoft.AspNetCore.Http;
using SparkNest.UI.MVC.Application.Abstractions;
using SparkNest.UI.MVC.Application.DTOs.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Application.Features.Blogs.Commands.Create
{
    public class BlogsCreateCommandRequest : IRequest<bool>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string CategoryId { get; set; }
        public IFormFile Photo { get; set; }
    }

    public class BlogsCreateCommandRequestHandler : IRequestHandler<BlogsCreateCommandRequest, bool>
    {
        IBlogService _blogService;

        public BlogsCreateCommandRequestHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<bool> Handle(BlogsCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _blogService.CreateBlogAsync(new BlogCreateDTO
            {
                Photo = request.Photo,
                CategoryId = request.CategoryId,
                Content = request.Content,
                Title = request.Title,
            });
            return result;
        }
    }
}
