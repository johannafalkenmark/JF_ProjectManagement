using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services;




public class MemberService(IUserRepository userRepository, IUserAddressRepository userAddressRepository, UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager) : IMemberService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserAddressRepository _userAddressRepository = userAddressRepository;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;


    public async Task<MemberResult> GetMembersAsync()
    {

        //lägg till var address = _useradrep.get all...

        var result = await _userRepository.GetAllAsync();
        return result.MapTo<MemberResult>();
    }

    public async Task<MemberResult> AddMemberToRole(string memberId, string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
            return new MemberResult { Succeeded = false, StatusCode = 404, Error = "Role does not exist" };

        var member = await _userManager.FindByIdAsync(memberId);
        if (member == null)
            return new MemberResult { Succeeded = false, StatusCode = 404, Error = "User does not exist" };


        var result = await _userManager.AddToRoleAsync(member, roleName);
        return result.Succeeded
             ? new MemberResult { Succeeded = true, StatusCode = 200 }
             : new MemberResult { Succeeded = false, StatusCode = 500, Error = "Unable to add user to role" };

    }

public async Task<MemberResult> CreateMemberAsync(UserSignUpForm signUpForm, string roleName = "User")
    {
        if (signUpForm == null)
             return new MemberResult { Succeeded = false, StatusCode = 400, Error = "Form data cant be null " };

        var existsResult = await _userRepository.AlreadyExistsAsync(x => x.Email == signUpForm.Email);
        if (existsResult.Succeeded)
            return new MemberResult { Succeeded = false, StatusCode = 409, Error = "User with same email already exists " };
    
    try
        {

            var userEntity = signUpForm.MapTo<UserEntity>();
            
            var result = await _userManager.CreateAsync(userEntity, signUpForm.Password);


            if (result.Succeeded)
            {
                var addToRoleResult = await AddMemberToRole(userEntity.Id, roleName);

                return result.Succeeded
        ? new MemberResult { Succeeded = true, StatusCode = 201 }
        : new MemberResult { Succeeded = false, StatusCode = 201, Error = "User created but not added to role" };

            }

                         return new MemberResult { Succeeded = false, StatusCode = 500, Error = "Unable to create user" };


        }

        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
                     return new MemberResult { Succeeded = false, StatusCode = 500, Error = ex.Message };

        }





    }




}

////Lägg till repository här om det ska användas:
//public class MemberService(UserManager<UserEntity> userManager) : IMemberService
//{


//    private readonly UserManager<UserEntity> _userManager = userManager;



//    ////lägg till skapa member
//    //public async Task<IEnumerable<Member>> GetMembersAsync()
//    //{
//    //    var list = await _userManager.Users.Include(x => x.Address).ToListAsync();
//    //    var members = list.Select(x => new Member
//    //    {
//    //        Id = x.Id,
//    //       ImageUrl = x.ImageUrl,
//    //        FirstName = x.FirstName,
//    //        LastName = x.LastName,
//    //        Email = x.Email,
//    //        JobTitle = x.JobTitle,
//    //        PhoneNumber = x.PhoneNumber,

//    //        Address = new MemberAddress
//    //        {
//    //            StreetName = x.Address?.StreetName,
//    //            PostalCode = x.Address?.PostalCode,
//    //            City = x.Address?.City,
//    //        }

//    //    });


//    //    return members;
//    //}


//    ////SKicka tillbaka EN Member:
//    //public async Task<Member> GetSingleMemberAsync() 
//    //{
//    //    var user = await _userManager.Users.Include(x => x.Address).FirstOrDefaultAsync();

//    //    if (user == null)
//    //        return null;

//    //    var member =  new Member
//    //    {
//    //        Id = user.Id,
//    //        ImageUrl = user.ImageUrl,
//    //        FirstName = user.FirstName,
//    //        LastName = user.LastName,
//    //        Email = user.Email,
//    //        JobTitle = user.JobTitle,
//    //        PhoneNumber = user.PhoneNumber,

//    //        Address = new MemberAddress
//    //        {
//    //            StreetName = user.Address?.StreetName,
//    //            PostalCode = user.Address?.PostalCode,
//    //            City = user.Address?.City,
//    //        }

//    //    };


//    //    return member;
//    //}
//}
