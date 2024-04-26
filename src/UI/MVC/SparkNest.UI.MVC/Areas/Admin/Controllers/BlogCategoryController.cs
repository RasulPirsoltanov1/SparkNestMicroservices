using MediatR;
using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Application.DTOs.Blog;
using SparkNest.UI.MVC.Application.Features.BlogCategory.Commands.Create;
using SparkNest.UI.MVC.Application.Features.BlogCategory.Commands.Delete;
using SparkNest.UI.MVC.Application.Features.Blogs.Queries.GetAllBlogCategories;

namespace SparkNest.UI.MVC.Areas.Admin.Controllers
{
    [Area(("Admin"))]
    public class BlogCategoryController : Controller
    {
        IMediator _mediator;

        public BlogCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var blogCategories = await _mediator.Send(new GetAllBlogCategoryQueryRequest());
            return View(blogCategories);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogCategoryCreateCommandRequest blogCategoryCreateCommandRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(blogCategoryCreateCommandRequest);
            }
            var result = await _mediator.Send(blogCategoryCreateCommandRequest);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(string categoryId)
        {
            var result = await _mediator.Send(new BlogCategoryDeleteCommandRequest()
            {
                CategoryId = categoryId
            });
            return RedirectToAction(nameof(Index));
        }


    }
}
