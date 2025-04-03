
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class AuthService(IMemberService memberService, SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager) : IAuthService
{
    private readonly IMemberService _memberService = memberService;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly UserManager<UserEntity> _userManager = userManager;



    public async Task<AuthResult> LoginAsync(UserLoginForm loginForm)
    {
        if (loginForm == null)
            return new AuthResult { Succeeded = false, StatusCode = 400, Error = "Not all required fields are filled." };

        var result = await _signInManager.PasswordSignInAsync(loginForm.Email, loginForm.Password, loginForm.IsPersistent, false);
        return result.Succeeded
           ? new AuthResult { Succeeded = true, StatusCode = 200 }
           : new AuthResult { Succeeded = false, StatusCode = 401, Error = "Invalid email or password." };


    }

    public async Task<AuthResult>SignUpAsync(UserSignUpForm signUpForm)

    {
        if (signUpForm == null)
            return new AuthResult { Succeeded = false, StatusCode = 400, Error = "Not all required fields are filled." };

        var result = await _memberService.CreateMemberAsync(signUpForm);
        return result.Succeeded
           ? new AuthResult { Succeeded = true, StatusCode = 201 }
           : new AuthResult { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };

    }

    public async Task<AuthResult> LogOutAsync()
    {
        await _signInManager.SignOutAsync();
        return new AuthResult { Succeeded = true, StatusCode = 200 };

    }
    public async Task<bool> AlreadyExistsAsync(string email)
    {
        var result = await _userManager.Users.AnyAsync(x => x.Email == email);
        return result;
    }


}

//    public async Task<bool> LoginAsync(UserLoginForm loginForm)
//    {
//        //LOGGAS IN
//        var result = await _signInManager.PasswordSignInAsync(loginForm.Email, loginForm.Password, loginForm.IsPersistent, false);
//        return result.Succeeded;
//    }




//    public async Task<bool>SignUpAsync(UserSignUpForm signUpForm)
//    {
//        var userEntity = new UserEntity()
//        {
//            UserName = signUpForm.Email,
//            FirstName = signUpForm.FirstName,
//            LastName = signUpForm.LastName,
//            Email = signUpForm.Email
//        };
//        var result = await _userManager.CreateAsync(userEntity, signUpForm.Password);
//        if (result.Succeeded)
//        {//BLIR INLOGGAD OM LYCKAS
//            await _signInManager.PasswordSignInAsync(signUpForm.Email, signUpForm.Password, false, false);
//        }
//        return result.Succeeded;
//    }


//    public async Task LogoutAsync()
//    {
//        await _signInManager.SignOutAsync();
//    }
//}
