using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Base;
using IMS.Domain.Entities;
using IMS.Domain.Utilities;
using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Base;
using IMS.Domain.Entities;
using IMS.Domain.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IMS.Business.Services
{
    public interface IRoleService
    {
        Task<RoleRes> CreateRoleAsync(RoleReq request);
        Task<RoleRes> UpdateRoleAsync(RoleReq request);
        Task<RoleRes> GetRoleByIdAsync(Guid id);
        Task<List<RoleRes>> GetAllRolesAsync(Pagination? pagination);
        Task DeleteRoleAsync(Guid id);
        Task<RoleAssign> AssignRoleToUser(Guid roleId, Guid userId);
        Task<RoleAssign> RemoveRoleFromUser(Guid roleId, Guid userId);
        Task<List<UserRoleRes>> GetUsersInRoleAsync(Guid roleId);
        Task AddClaimToRole(Guid roleId, string resource, string action, string accessLevel);
        Task RemoveClaimFromRole(Guid roleId, string resource);
        Task<bool> UserHasPermission(string userId, string resource, string action);

    }

    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleService(
            RoleManager<AppRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<RoleRes> CreateRoleAsync(RoleReq request)
        {
            var appRole = new AppRole
            {
                Id = request.Id,
                Name = request.Name,

            };

            var result = await _roleManager.CreateAsync(appRole);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create role: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            if (request.Claims != null && request.Claims.Any())
            {
                foreach (var claim in request.Claims)
                {
                    await _roleManager.AddClaimAsync(appRole, new Claim(claim.ClaimType, claim.ClaimValue));
                }
            }

            return new RoleRes
            {
                Id = appRole.Id,
                Name = appRole.Name,

                Claims = request.Claims
            };
        }

        public async Task<RoleRes> UpdateRoleAsync(RoleReq request)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null) throw new KeyNotFoundException("Role not found");

            role.Name = request.Name;


            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to update role: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            // Update Claims
            var existingClaims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in existingClaims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }

            if (request.Claims != null && request.Claims.Any())
            {
                foreach (var claim in request.Claims)
                {
                    await _roleManager.AddClaimAsync(role, new Claim(claim.ClaimType, claim.ClaimValue));
                }
            }

            return new RoleRes
            {
                Id = role.Id,
                Name = role.Name,

                Claims = request.Claims
            };
        }

        public async Task<RoleRes> GetRoleByIdAsync(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null) throw new KeyNotFoundException("Role not found");

            var claims = await _roleManager.GetClaimsAsync(role);
            var claimsList = claims.Select(c => new RoleClaimReq
            {
                RoleId = role.Id,
                ClaimType = c.Type,
                ClaimValue = c.Value
            }).ToList();

            return new RoleRes
            {
                Id = role.Id,
                Name = role.Name,

                Claims = claimsList
            };
        }

        public async Task<List<RoleRes>> GetAllRolesAsync(Pagination? pagination)
        {
            pagination ??= new Pagination();
            var total = 0;
            var totalPages = 0;

            var paginatedRoles = await _roleManager.Roles
                .OrderBy(r => r.Name)
                .Paginate((int)pagination.PageIndex, (int)pagination.PageSize, ref total, ref totalPages)
                .ToListAsync();

            pagination.Total = total;
            pagination.TotalPages = totalPages;

            var roleResponses = new List<RoleRes>();

            foreach (var role in paginatedRoles)
            {
                var claims = await _roleManager.GetClaimsAsync(role);
                var claimsList = claims.Select(c => new RoleClaimReq
                {
                    RoleId = role.Id,
                    ClaimType = c.Type,
                    ClaimValue = c.Value
                }).ToList();

                roleResponses.Add(new RoleRes
                {
                    Id = role.Id,
                    Name = role.Name,

                    Claims = claimsList
                });
            }

            return roleResponses;
        }

        public async Task DeleteRoleAsync(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null) throw new KeyNotFoundException("Role not found");

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to delete role: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        public async Task<RoleAssign> AssignRoleToUser(Guid roleId, Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new Exception("User not found.");

            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) throw new Exception("Role not found.");

            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to assign role: {errors}");
            }

            return new RoleAssign
            {
                RoleId = roleId,
                UserId = userId
            };
        }

        public async Task<RoleAssign> RemoveRoleFromUser(Guid roleId, Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new Exception("User not found.");

            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) throw new Exception("Role not found.");

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to remove role: {errors}");
            }

            return new RoleAssign
            {
                RoleId = roleId,
                UserId = userId
            };
        }

        public async Task<List<UserRoleRes>> GetUsersInRoleAsync(Guid roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) throw new KeyNotFoundException("Role not found");

            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            return users.Select(u => new UserRoleRes
            {
                UserId = u.Id,
                UserName = u.UserName,
                Email = u.Email
            }).ToList();
        }

        public async Task AddClaimToRole(Guid roleId, string resource, string action, string accessLevel)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) throw new KeyNotFoundException("Role not found");

            var claimValue = Claimstore.GenerateAccessClaim(
                (accessLevel, resource, new[] { action }));

            var claim = new Claim(Claimstore.ResourceClaim(resource), claimValue);
            await _roleManager.AddClaimAsync(role, claim);
        }

        public async Task RemoveClaimFromRole(Guid roleId, string resource)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) throw new KeyNotFoundException("Role not found");

            var claims = await _roleManager.GetClaimsAsync(role);
            var claimToRemove = claims.FirstOrDefault(c => c.Type == Claimstore.ResourceClaim(resource));

            if (claimToRemove != null)
            {
                await _roleManager.RemoveClaimAsync(role, claimToRemove);
            }
        }

        public async Task<bool> UserHasPermission(string userId, string resource, string action)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var roleName in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    var claims = await _roleManager.GetClaimsAsync(role);
                    var resourceClaim = claims.FirstOrDefault(c => c.Type == Claimstore.ResourceClaim(resource));

                    if (resourceClaim != null)
                    {
                        var permissions = Claimstore.ExtractResourceClaim(resourceClaim.Value);
                        if (permissions.Any(p => p.Contains(action)))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }


    }

    public class RoleAssign
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
    }

    public class UserRoleRes
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}