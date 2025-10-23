using ZMS.API.Middleware;
using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Services;
using IMS.Domain.Utilities;
using IMS.Business.DTOs.Requests;
using IMS.Business.Services;
using IMS.Domain.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading.Tasks;
using ZMS.API.Middleware;

namespace IMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Authorize(Roles = "SuperAdmin")]

    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Permission("Role", "Read")]
        public async Task<IActionResult> GetAllRoles([FromQuery] Pagination pagination)
        {
            try
            {
                var roles = await _roleService.GetAllRolesAsync(pagination);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [Permission("Role", "Read")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            try
            {
                var role = await _roleService.GetRoleByIdAsync(id);
                return Ok(role);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPost]
        [Permission("Role", "Create")]
        public async Task<IActionResult> CreateRole([FromBody] RoleReq request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var role = await _roleService.CreateRoleAsync(request);
                return CreatedAtAction(nameof(GetRoleById), new { id = role.Id }, role);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Permission("Role", "Update")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] RoleReq request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                request.Id = id;
                var role = await _roleService.UpdateRoleAsync(request);
                return Ok(role);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Permission("Role", "Delete")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            try
            {
                await _roleService.DeleteRoleAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPost("assign")]
        [Permission("Role", "Update")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _roleService.AssignRoleToUser(request.RoleId, request.UserId);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPost("remove")]
        [Permission("Role", "Update")]
        public async Task<IActionResult> RemoveRoleFromUser([FromBody] RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _roleService.RemoveRoleFromUser(request.RoleId, request.UserId);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpGet("{roleId}/users")]
        [Permission("Role", "Read")]
        public async Task<IActionResult> GetUsersInRole(Guid roleId)
        {
            try
            {
                var users = await _roleService.GetUsersInRoleAsync(roleId);
                return Ok(users);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPost("{roleId}/claims")]
        [Permission("Role", "Update")]
        public async Task<IActionResult> AddClaimToRole(Guid roleId, [FromBody] AddClaimRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _roleService.AddClaimToRole(roleId, request.Resource, request.Action, request.AccessLevel);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpDelete("{roleId}/claims/{resource}")]
        [Permission("Role", "Update")]
        public async Task<IActionResult> RemoveClaimFromRole(Guid roleId, string resource)
        {
            try
            {
                await _roleService.RemoveClaimFromRole(roleId, resource);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }



        public class RoleAssignRequest
        {
            public Guid RoleId { get; set; }
            public Guid UserId { get; set; }
        }

        public class AddClaimRequest
        {
            public string Resource { get; set; }
            public string Action { get; set; }
            public string AccessLevel { get; set; }
        }
    }
}