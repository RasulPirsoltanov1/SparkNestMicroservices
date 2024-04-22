using MediatR;
using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Application.Abstractions;
using SparkNest.UI.MVC.Application.Features.Blogs.Queries.GetAll;

namespace SparkNest.UI.MVC.ViewComponents.UI
{
    public class HomePageBlogsVC:ViewComponent
    {
        IMediator _mediator;

        public HomePageBlogsVC(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var blogs = await _mediator.Send(new BlogsGetAllQueryRequest());
            return View(blogs.OrderByDescending(x=>x.CreateDate).Take(3).ToList());
        }
    }
}
