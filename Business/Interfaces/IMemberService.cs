using Business.Models;
using Domain.Models;

namespace Business.Interfaces
{
    public interface IMemberService
    {
        Task<MemberResult> AddMemberToRole(string memberId, string roleName);
        Task<MemberResult> CreateMemberAsync(UserSignUpForm signUpForm, string roleName = "User");
        Task<MemberResult> CreateMemberManuallyAsync(AddMemberForm addMemberForm, string roleName = "User");
        Task<string> GetDisplayNameAsync(string memberId);
        Task<MemberResult<Member>> GetMemberByIdAsync(string id);

        //Task<MemberResult> GetMembersAsync();
        Task<MemberResult<IEnumerable<Member>>> GetMembersAsync();
    }
}