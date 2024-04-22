using MediatR;
using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Application.Features.Blogs.Queries.GetAll;
using SparkNest.UI.MVC.Application.Features.Blogs.Queries.GetById;

namespace SparkNest.UI.MVC.Controllers
{
    public class BlogController : Controller
    {
        IMediator _mediator;

        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _mediator.Send(new BlogsGetAllQueryRequest());
            return View(blogs);
        }
        public async Task<IActionResult> Detail(string Id)
        {
            var blog = await _mediator.Send(new BlogGetByIdQueryRequest()
            {
                BlogId=Id,
            });
            return View(blog);
        }
    }
}
