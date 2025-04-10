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


    public async Task<MemberResult<IEnumerable<Member>>> GetMembersAsync()
    {
        var response = await _userRepository.GetAllAsync
      ( //lägger in filtrering, sortering här:
      orderByDescending: true

      );
        return new MemberResult<IEnumerable<Member>> { Succeeded = true, StatusCode = 201, Result = response.Result };
       
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
          
            userEntity.UserName = signUpForm.Email; 
            userEntity.Email = userEntity.UserName;

            

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


    public async Task<MemberResult> CreateMemberManuallyAsync(AddMemberForm addMemberForm, string roleName = "User")
    {
        if (addMemberForm == null)
            return new MemberResult { Succeeded = false, StatusCode = 400, Error = "Form data cant be null " };

        var existsResult = await _userRepository.AlreadyExistsAsync(x => x.Email == addMemberForm.Email);
        if (existsResult.Succeeded)
            return new MemberResult { Succeeded = false, StatusCode = 409, Error = "User with same email already exists " };

        try
        {

            var userEntity = addMemberForm.MapTo<UserEntity>();

            userEntity.UserName = addMemberForm.Email;
            userEntity.Email = userEntity.UserName;

            var result = await _userManager.CreateAsync(userEntity);


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

    public async Task<MemberResult<Member>> GetMemberByIdAsync(string id)
    {
        var response = await _userRepository.GetAsync(x => x.Id == id);
        var entity = response.Result;
        if (entity == null)
            return new MemberResult<Member> { Succeeded = false, StatusCode = 404, Error = $"User with id '{id}' was not found" };

        var user = entity.MapTo<Member>();
        return new MemberResult<Member> { Succeeded = true, StatusCode = 200, Result = user };
    }

    public async Task<string> GetDisplayNameAsync(string memberId)
    {
        if (string.IsNullOrEmpty(memberId))
            return "";
        var member = await _userManager.FindByIdAsync(memberId);
        return member == null ? "" : $"{member.FirstName} {member.LastName}";
    }


}


