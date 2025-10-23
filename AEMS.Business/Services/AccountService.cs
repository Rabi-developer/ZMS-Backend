using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Base;
using IMS.Domain.Context;
using IMS.Domain.Entities;
using IMS.Domain.Utilities;
using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Services;
using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Context;
using IMS.Domain.Entities;
using IMS.Domain.Utilities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace IMS.Business.Services
{
    public interface IAccountService : IBaseMinService
    {
        Task<Response<LoginRes>> Login(LoginReq loginReq);
        Task<Response<LoginRes>> Register(SignUpReq registerReq);
        Task<Response<ApplicationUserRes>> CreateWithRole(CreateUserWithRoleReq userReq);
        Task<Response<ApplicationUserRes>> UpdateApplicationUser(UpdateUserReq userReq);
        public Task<Response<IList<ApplicationUserRes>>> GetAll(Pagination? pagination);
    }

    public class AccountService : BaseMinService<AppUserRepository>, IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            RoleManager<AppRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            ApplicationDbContext context) : base(unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<Response<LoginRes>> Login(LoginReq loginReq)
        {
            ApplicationUser? user = Helpers.EmailRegex().IsMatch(loginReq.Username)
                ? await _userManager.FindByEmailAsync(loginReq.Username)
                : await _userManager.FindByNameAsync(loginReq.Username);

            if (user == null)
            {
                return new Response<LoginRes>
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    StatusMessage = "User not found"
                };
            }

            if (!user.IsActive)
            {
                return new Response<LoginRes>
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    StatusMessage = "User account is inactive"
                };
            }

            var result = await _signInManager.PasswordSignInAsync(
                loginReq.Username,
                loginReq.Password,
                true,
                false);

            if (!result.Succeeded)
            {
                return new Response<LoginRes>
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    StatusMessage = "Invalid credentials"
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = await (from ur in UnitOfWork._context.UserRoles
                                    join r in UnitOfWork._context.Roles on ur.RoleId equals r.Id
                                    join c in UnitOfWork._context.RoleClaims on r.Id equals c.RoleId
                                    where ur.UserId == user.Id
                                    select new { Claims = c, roles = r }).ToListAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FullName", $"{user.FirstName} {user.LastName}"),
                new Claim("IsActive", user.IsActive.ToString())
            };

            // Add role claims
            claims.AddRange(roleClaims
                .SelectMany(rc => rc.Claims.ClaimValue.Split(",")
                    .Select(v => new Claim(rc.Claims.ClaimType, v.Trim()))));

            // Add roles
            claims.Add(new Claim(ClaimTypes.Role, string.Join(",", roles)));

            var token = GenerateToken(claims);

            var response = new LoginRes
            {
                Token = token,
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                Roles = roles.ToList(),
                Permissions = roleClaims
                    .GroupBy(rc => rc.Claims.ClaimType)
                    .ToDictionary(
                        g => g.Key,
                        g => g.SelectMany(rc => rc.Claims.ClaimValue.Split(",").Select(v => v.Trim())).Distinct().ToList())
            };

            return new Response<LoginRes>
            {
                Data = response,
                StatusCode = HttpStatusCode.OK,
                StatusMessage = "Login successful"
            };
        }

        public async Task<Response<LoginRes>> Register(SignUpReq registerReq)
        {


            using var transaction = await UnitOfWork.BeginTransactionAsync();
            try
            {
                var existingUser = await _userManager.FindByEmailAsync(registerReq.Email)
                                 ?? await _userManager.FindByNameAsync(registerReq.UserName);

                if (existingUser != null)
                {
                    return new Response<LoginRes>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        StatusMessage = "User with this email or username already exists"
                    };
                }

                var user = new ApplicationUser
                {
                    UserName = registerReq.UserName,
                    Email = registerReq.Email,
                    FirstName = registerReq.FirstName,
                    LastName = registerReq.LastName,
                    MiddleName = registerReq.MiddleName,
                    EmailConfirmed = true,
                    IsActive = true,
                    IsDeleted = false,
                    NormalizedEmail = registerReq.Email.ToUpper(),
                    NormalizedUserName = registerReq.UserName.ToUpper(),
                };

                var createResult = await _userManager.CreateAsync(user, registerReq.Password);
                if (!createResult.Succeeded)
                {
                    return new Response<LoginRes>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        StatusMessage = string.Join(", ", createResult.Errors.Select(e => e.Description))
                    };
                }

                // Assign default role
                await _userManager.AddToRoleAsync(user, Constants.User);

                await _signInManager.SignInAsync(user, isPersistent: false);
                await UnitOfWork.CommitTransactionAsync(transaction);

                // Generate token with claims
                var loginResult = await Login(new LoginReq
                {
                    Username = registerReq.UserName,
                    Password = registerReq.Password
                });

                return loginResult;
            }
            catch (Exception ex)
            {
                await UnitOfWork.RollBackTransactionAsync();
                return new Response<LoginRes>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                };
            }
        }

        public async Task<Response<ApplicationUserRes>> CreateWithRole(CreateUserWithRoleReq userReq)
        {
            if (userReq.Role == "SuperAdmin")
            {
                return new Response<ApplicationUserRes>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    StatusMessage = "Super Admin Not Allowed."
                };
            }
            using var transaction = await UnitOfWork.BeginTransactionAsync();
            try
            {
                var existingUser = await _userManager.FindByEmailAsync(userReq.Email)
                                 ?? await _userManager.FindByNameAsync(userReq.UserName);

                if (existingUser != null)
                {
                    return new Response<ApplicationUserRes>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        StatusMessage = "User with this email or username already exists"
                    };
                }

                // Verify role exists
                var role = await _roleManager.FindByNameAsync(userReq.Role);
                if (role == null)
                {
                    return new Response<ApplicationUserRes>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        StatusMessage = "Specified role does not exist"
                    };
                }

                var user = new ApplicationUser
                {
                    UserName = userReq.UserName,
                    Email = userReq.Email,
                    FirstName = userReq.FirstName,
                    LastName = userReq.LastName,
                    MiddleName = userReq.MiddleName,
                    EmailConfirmed = true,
                    IsActive = true,
                    IsDeleted = false,
                    NormalizedEmail = userReq.Email.ToUpper(),
                    NormalizedUserName = userReq.UserName.ToUpper(),
                };

                var createResult = await _userManager.CreateAsync(user, userReq.Password);
                if (!createResult.Succeeded)
                {
                    return new Response<ApplicationUserRes>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        StatusMessage = string.Join(", ", createResult.Errors.Select(e => e.Description))
                    };
                }

                // Assign specified role
                await _userManager.AddToRoleAsync(user, userReq.Role);

                await UnitOfWork.CommitTransactionAsync(transaction);

                return new Response<ApplicationUserRes>
                {
                    Data = user.Adapt<ApplicationUserRes>(),
                    StatusCode = HttpStatusCode.Created,
                    StatusMessage = "User created successfully"
                };
            }
            catch (Exception ex)
            {
                await UnitOfWork.RollBackTransactionAsync();
                return new Response<ApplicationUserRes>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                };
            }
        }

        public async Task<Response<ApplicationUserRes>> UpdateApplicationUser(UpdateUserReq userReq)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userReq.Id.ToString());
                if (user == null)
                {
                    return new Response<ApplicationUserRes>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        StatusMessage = "User not found"
                    };
                }

                // Check if email/username is being changed to an existing one
                var existingUser = await _userManager.FindByEmailAsync(userReq.Email)
                                  ?? await _userManager.FindByNameAsync(userReq.UserName);

                if (existingUser != null && existingUser.Id != user.Id)
                {
                    return new Response<ApplicationUserRes>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        StatusMessage = "User with this email or username already exists"
                    };
                }

                // Update user properties
                user.UserName = userReq.UserName;
                user.Email = userReq.Email;
                user.FirstName = userReq.FirstName;
                user.LastName = userReq.LastName;
                user.MiddleName = userReq.MiddleName;
                user.NormalizedEmail = userReq.Email.ToUpper();
                user.NormalizedUserName = userReq.UserName.ToUpper();

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    return new Response<ApplicationUserRes>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        StatusMessage = string.Join(", ", updateResult.Errors.Select(e => e.Description))
                    };
                }

                return new Response<ApplicationUserRes>
                {
                    Data = user.Adapt<ApplicationUserRes>(),
                    StatusCode = HttpStatusCode.OK,
                    StatusMessage = "User updated successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<ApplicationUserRes>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                };
            }
        }

        private string GenerateToken(IEnumerable<Claim> claims, string? issuer = null, string? audience = null)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secrets.AuthenticationSchemeSecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer ?? Secrets.AuthenticationSchemeIssuer,
                audience: audience ?? Secrets.AuthenticationSchemeAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Response<IList<ApplicationUserRes>>> GetAll(Pagination? pagination)
        {
            // Ensure pagination is not null
            int pageNumber = pagination?.PageNumber ?? 1;
            int pageSize = pagination?.PageSize ?? 10;

            // Ensure valid positive values
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            // Query users with pagination
            var usersQuery = _userManager.Users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            var usersList = await usersQuery.ToListAsync();

            var result = new List<ApplicationUserRes>();

            foreach (var user in usersList)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var mappedUser = new ApplicationUserRes
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MiddleName = user.MiddleName,
                    IsActive = user.IsActive,
                    Roles = roles.ToList()
                };

                result.Add(mappedUser);
            }

            return new Response<IList<ApplicationUserRes>>
            {
                Data = result,

                StatusMessage = "Users retrieved successfully"
            };
        }

    }
}