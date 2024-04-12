using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Application.Abstractions;
using SparkNest.UI.MVC.Application.DTOs;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Controllers
{
    public class CommentController : Controller
    {
        IUserService _userService;
        ICommentService _commentService;
        public CommentController(IUserService userService, ICommentService commentService)
        {
            _userService = userService;
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentDTO createCommentDTO)
        {
            var user =await _userService.GetUser();
            createCommentDTO.UserName = user.UserName;
            createCommentDTO.UserId = user.Id;
            createCommentDTO.PhotoUrl = user.ImageFileStockUrl;
            await _commentService.Create(createCommentDTO);
            return RedirectToAction("Detail", "Home",new
            {
                Id =createCommentDTO.ProductId
            });
        }
    }
}
