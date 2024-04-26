using MediatR;
using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Application.Abstractions;
using SparkNest.UI.MVC.Application.Features.Blogs.Commands.Create;
using SparkNest.UI.MVC.Application.Features.Blogs.Commands.DeleteBlog;
using SparkNest.UI.MVC.Application.Features.Blogs.Commands.Update;
using SparkNest.UI.MVC.Application.Features.Blogs.Queries.GetAll;
using SparkNest.UI.MVC.Application.Features.Blogs.Queries.GetAllBlogCategories;
using SparkNest.UI.MVC.Application.Features.Blogs.Queries.GetById;
using SparkNest.UI.MVC.Helpers;
using System.Net.WebSockets;

namespace SparkNest.UI.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        IMediator _mediator;
        FileStockHelper _fileStockHelper;
        public BlogController(IMediator mediator, FileStockHelper fileStockHelper)
        {
            _mediator = mediator;
            _fileStockHelper = fileStockHelper;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _mediator.Send(new BlogsGetAllQueryRequest());
            return View(blogs);
        }

        public async Task<IActionResult> Delete(string blogId)
        {
            var result = await _mediator.Send(new BlogDeleteCommandRequest()
            {
                BlogId = blogId
            });
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _mediator.Send(new GetAllBlogCategoryQueryRequest());
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogsCreateCommandRequest blogsCreateCommandRequest)
        {
            ViewBag.Categories = await _mediator.Send(new GetAllBlogCategoryQueryRequest());
            if (!ModelState.IsValid)
                return View(blogsCreateCommandRequest);
            var result = await _mediator.Send(blogsCreateCommandRequest);
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Update(string blogId)
        {
            ViewBag.Categories = await _mediator.Send(new GetAllBlogCategoryQueryRequest());
            var blog = await _mediator.Send(new BlogGetByIdQueryRequest()
            {
                BlogId = blogId
            });
            return View(new BlogUpdateCommandRequest
            {
                CategoryId = blog.CategoryId,
                Content = blog.Content,
                Id = blog.Id,
                PhotoUrl = blog.PhotoUrl,
                Title = blog.Title,
                PhotoFileStockUrl = blog.PhotoFileStockUrl,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(BlogUpdateCommandRequest blogUpdateCommandRequest)
        {
            ViewBag.Categories = await _mediator.Send(new GetAllBlogCategoryQueryRequest());

            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _mediator.Send(blogUpdateCommandRequest);
            return RedirectToAction(nameof(Index));
        }
    }
}
