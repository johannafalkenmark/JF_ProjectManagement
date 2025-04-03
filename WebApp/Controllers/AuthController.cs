using Business.Interfaces;
using Business.Services;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace WebApp.Controllers;

public class AuthController (IAuthService authService) : Controller
{

    public readonly IAuthService _authService = authService;
    public IActionResult Login(string returnUrl = "~/")
    {
        ViewBag.ErrorMessage = null;
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginForm userLoginForm, string returnUrl ="~/")
    {
        ViewBag.ReturnUrl = returnUrl;


        if (!ModelState.IsValid)
            return View(userLoginForm);


        var loginForm = userLoginForm.MapTo<UserLoginForm>();
        var result = await _authService.LoginAsync(loginForm);

        if (result.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            ViewBag.ErrorMessage = "Something went wrong. Try again later.";
        }


        return View(userLoginForm);
    }




    public IActionResult SignUp(string returnUrl = "~/")
    {
        ViewBag.ErrorMessage = null;
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(UserSignUpForm userSignUpForm, string returnUrl = "~/")
    {

        if (!ModelState.IsValid)
            return View(userSignUpForm);
        
            if (await _authService.AlreadyExistsAsync(userSignUpForm.Email))
            {
                ViewBag.ErrorMessage = "A user with the same email already exists.";
                return View(userSignUpForm);
            }


            var signUpForm = userSignUpForm.MapTo<UserSignUpForm>();
            var result = await _authService.SignUpAsync(signUpForm);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                ViewBag.ErrorMessage = "Something went wrong. Try again later.";
            }
        

        return View(userSignUpForm);
    }



    public async Task<IActionResult> Logout()
    {
        await _authService.LogOutAsync();
        return LocalRedirect("~/");
    }
}
