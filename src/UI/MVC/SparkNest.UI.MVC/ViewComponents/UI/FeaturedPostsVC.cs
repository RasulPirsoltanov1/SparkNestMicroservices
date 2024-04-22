using MediatR;
using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Application.Features.Blogs.Queries.GetAll;

namespace SparkNest.UI.MVC.ViewComponents.UI
{
    public class FeaturedPostsVC:ViewComponent
    {
        IMediator _mediator;

        public FeaturedPostsVC(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var featuredBlogs = await _mediator.Send(new BlogsGetAllQueryRequest());
            return View(featuredBlogs);
        }
    }
}
