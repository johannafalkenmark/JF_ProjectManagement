using Business.Models;
using Domain.Models;

namespace Business.Interfaces;

public interface IAuthService
{
    Task<bool> AlreadyExistsAsync(string email);

    Task<AuthResult> LoginAsync(UserLoginForm loginForm);
    Task<AuthResult> LogOutAsync();
    Task<AuthResult> SignUpAsync(UserSignUpForm signUpForm);
}