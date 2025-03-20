using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IUsersService
{
    Task<List<ApplicationUserReq>> GetAllUsersAsync();
    Task<ApplicationUserReq> GetUserByIdAsync(Guid userId);
    Task<ApplicationUserReq> CreateUserAsync(SignUpReq signUpRequest);
    Task<ApplicationUserReq> UpdateUserAsync(Guid userId, SignUpReq signUpRequest);
    Task DeleteUserAsync(Guid userId);
}


public class UsersService : IUsersService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public UsersService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ApplicationUserReq>> GetAllUsersAsync()
    {
        var users = _userManager.Users.ToList();
        var userDtos = new List<ApplicationUserReq>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var roleName = roles.FirstOrDefault() ?? string.Empty;

            userDtos.Add(new ApplicationUserReq
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Role = roleName
            });
        }

        return userDtos;
    }

    public async Task<ApplicationUserReq> GetUserByIdAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) throw new KeyNotFoundException("User not found.");

        var roles = await _userManager.GetRolesAsync(user);
        var roleName = roles.FirstOrDefault() ?? string.Empty;

        return new ApplicationUserReq
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            MiddleName = user.MiddleName,
            LastName = user.LastName,
            Role = roleName
        };
    }

    public async Task<ApplicationUserReq> CreateUserAsync(SignUpReq signUpRequest)
    {
        var user = new ApplicationUser
        {
            UserName = signUpRequest.UserName,
            Email = signUpRequest.Email,
            FirstName = signUpRequest.FirstName,
            LastName = signUpRequest.LastName,
            MiddleName = signUpRequest.MiddleName
        };

        var result = await _userManager.CreateAsync(user, signUpRequest.Password);
        if (!result.Succeeded)
        {
            throw new Exception($"User creation failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        // Save changes via UnitOfWork
        await _unitOfWork.SaveAsync();

        return new ApplicationUserReq
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };
    }

    public async Task<ApplicationUserReq> UpdateUserAsync(Guid userId, SignUpReq signUpRequest)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) throw new KeyNotFoundException("User not found.");

        user.UserName = signUpRequest.UserName;
        user.Email = signUpRequest.Email;
        user.FirstName = signUpRequest.FirstName;
        user.LastName = signUpRequest.LastName;
        user.MiddleName = signUpRequest.MiddleName;
      

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception($"User update failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        // Save changes via UnitOfWork
        await _unitOfWork.SaveAsync();

        return new ApplicationUserReq
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) throw new KeyNotFoundException("User not found.");

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception($"User deletion failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        // Save changes via UnitOfWork
        await _unitOfWork.SaveAsync();
    }
}

