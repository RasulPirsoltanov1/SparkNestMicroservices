using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Application.Abstractions;
using SparkNest.UI.MVC.Models.Comment;

namespace SparkNest.UI.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            var comments = (await _commentService.GetAll()).Select(x=>new CommentVM
            {
                Content=x.Content,
                PhotoUrl=x.PhotoUrl,
                ProductId=x.ProductId,
                UserName=x.UserName,
                UserId = x.UserId,
                Id=x.Id
            }).ToList();
            return View(comments);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            await _commentService.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
