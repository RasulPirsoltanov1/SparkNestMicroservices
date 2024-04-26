using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Application.Abstractions;
using SparkNest.UI.MVC.Models.Comment;

namespace SparkNest.UI.MVC.ViewComponents.UI
{
    public class ProductCommentsVC:ViewComponent
    {
        ICommentService _commentService;

        public ProductCommentsVC(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string productId)
        {
            ViewBag.PId = productId;
            return View((await _commentService.GetAllByProductId(productId)).Select(x=>new CommentVM
            {
                ProductId = productId,
                Content=x.Content,
                PhotoUrl=x.PhotoUrl,
                UserId=x.UserId,
                UserName=x.UserName,
                CreateDate=x.CreateDate
            }).ToList());
        }
    }
}
