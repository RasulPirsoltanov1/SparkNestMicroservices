using MediatR;
using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Application.Features.Blogs.Queries.GetAllBlogCategories;

namespace SparkNest.UI.MVC.ViewComponents.UI
{
    public class TopCategoriesVC : ViewComponent
    {
        IMediator _mediator;

        public TopCategoriesVC(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _mediator.Send(new GetAllBlogCategoryQueryRequest());
            return View(categories.Take(5).ToList());
        }
    }
}
