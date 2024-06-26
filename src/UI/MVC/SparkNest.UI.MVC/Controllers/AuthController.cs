﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SparkNest.UI.MVC.Models;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Controllers
{
    public class AuthController : Controller
    {
        IIdentityService _identityService;
        IHttpContextAccessor _contextAccessor;

        public AuthController(IIdentityService identityService, IHttpContextAccessor contextAccessor)
        {
            _identityService = identityService;
            _contextAccessor = contextAccessor;
        }

        public IActionResult SignIn(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpInput signUpInput)
        {
            signUpInput.Country = "AZE";
            await _identityService.SignUpAsync(signUpInput);
            return RedirectToAction(nameof(SignIn));
        }
        public async Task<IActionResult> SignOut()
        {
            await _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _identityService.RevokeRefreshToken();
            return RedirectToAction("Index", "HomePage");

        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInInput signInInput)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(signInInput);
                }
                var result = await _identityService.SignIn(signInInput);
                if (result.Errors != null)
                {
                    result.Errors.ForEach(error =>
                    {
                        ModelState.AddModelError("", error);
                    });
                    return View(signInInput);
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
            string returnUrl = Request.Form["ReturnUrl"];
            if (returnUrl is not null)
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "User");
        }
    }
}
