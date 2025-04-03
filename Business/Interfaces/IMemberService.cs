using Business.Models;
using Domain.Models;

namespace Business.Interfaces
{
    public interface IMemberService
    {
        Task<MemberResult> AddMemberToRole(string memberId, string roleName);
        Task<MemberResult> CreateMemberAsync(UserSignUpForm signUpForm, string roleName = "User");
        Task<MemberResult> GetMembersAsync();
    }
}