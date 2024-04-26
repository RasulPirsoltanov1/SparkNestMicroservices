using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Application.DTOs;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetUser();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(UploadProfileImageDTO uploadProfileImageDTO)
        {
            uploadProfileImageDTO.UserNameOrEmail = User.Identity.Name;
            await _userService.UploadImageAsync(uploadProfileImageDTO);
            return RedirectToAction(nameof(Index));
        }

    }
}
