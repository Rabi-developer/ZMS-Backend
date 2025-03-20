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
        Task<RoleAssign> AssignUser(Guid RoleId, Guid UserId);
        IList<ResPerm> GetAllResources();
    }

    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleService(RoleManager<AppRole> roleManager, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
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
                Name = request.Name
            };

            var result = await _roleManager.CreateAsync(appRole);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create role: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            // Add role claims if they exist
            if (request.Claims != null && request.Claims.Any())
            {
                foreach (var claim in request.Claims)
                {
                    await _roleManager.AddClaimAsync(appRole, new System.Security.Claims.Claim(claim.ClaimType, claim.ClaimValue));
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

            // Update claims
            var existingClaims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in existingClaims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }

            if (request.Claims != null && request.Claims.Any())
            {
                foreach (var claim in request.Claims)
                {
                    await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(claim.ClaimType, claim.ClaimValue));
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
            pagination ??= new Pagination(); // Default to a new Pagination object if null
            var total = 0;
            var totalPages = 0;

            // Apply pagination to roles
            var paginatedRoles = await _roleManager.Roles
                .OrderBy(r => r.Name) // Sorting for consistent pagination
                .Paginate((int)pagination.PageIndex, (int)pagination.PageSize, ref total, ref totalPages)
                .ToListAsync();

            // Update pagination details
            pagination.Total = total;
            pagination.TotalPages = totalPages;

            // Map roles and claims
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

        public async Task<RoleAssign> AssignUser(Guid RoleId, Guid UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            if (user == null)
                throw new Exception("User not found.");

            // Fetch the role using RoleManager
            var role = await _roleManager.FindByIdAsync(RoleId.ToString());
            if (role == null)
                throw new Exception("Role not found.");

            // Check if the user is already assigned to this role
            var currentRoles = await _userManager.GetRolesAsync(user);
            var currentRole = currentRoles.FirstOrDefault(r => r == role.Name);

            if (currentRole != null)
            {
                // If the user is already assigned to this role, remove it
                var removeResult = await _userManager.RemoveFromRoleAsync(user, role.Name);
                if (!removeResult.Succeeded)
                {
                    var errors = string.Join(", ", removeResult.Errors.Select(e => e.Description));
                    throw new Exception($"Failed to remove the role: {errors}");
                }
            }

            // Assign the new role to the user
            var addResult = await _userManager.AddToRoleAsync(user, role.Name);
            if (!addResult.Succeeded)
            {
                var errors = string.Join(", ", addResult.Errors.Select(e => e.Description));
                throw new Exception($"Failed to assign the new role: {errors}");
            }

            // Return the RoleAssign object
            return new RoleAssign
            {
                RoleId = RoleId,
                UserId = UserId
            };
        }

        public IList<ResPerm> GetAllResources()
        {
            return ClaimStore.Resources.List;
        }
    }
}
