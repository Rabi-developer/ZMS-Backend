using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Base;
using IMS.Domain.Context;
using IMS.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IMS.Business.Services;
public interface IAccountService : IBaseMinService
{
    Task<Response<LoginRes>> Login(LoginReq loginReq);
    Task<Response<LoginRes>> Register(SignUpReq registerReq);
    Task<Response<ApplicationUserRes>> CreateWithRole(ApplicationUserReq userReq);
    Task<Response<ApplicationUserRes>> UpdateApplicationUser(ApplicationUserReq userReq);
    //Task<IActionResult> Register(RegisterReq registerReq);
    //Task<IActionResult> ConfirmEmail(string email, string token);
    //Task<IActionResult> ResendVerificationEmail();
}
public class AccountService : BaseMinService<AppUserRepository>, IAccountService
{
    private readonly IConfiguration _configuration;
    //private readonly IEmailSender _emailSender;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountService(
        IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager,
        RoleManager<AppRole> roleManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration,
        ApplicationDbContext context
        //IEmailSender emailSender,
        //IHttpContextAccessor httpContextAccessor
        ) : base(unitOfWork)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _configuration = configuration;
        //_emailSender = emailSender;
    }

    /// <summary>
    /// Asynchronously logs in a user.
    /// </summary>
    /// <param name="loginReq">The login request containing the user's credentials.</param>
    /// <returns>An IActionResult that represents the result of the login operation.</returns>
    /// <remarks>
    /// This method first checks if the username in the login request is an email address. If it is, it attempts to find a user with that email address. If it's not, it attempts to find a user with that username.
    /// If a user is not found, it returns a "User Not Found!" message.
    /// If the user is found but is not active, it returns a "You are Disabled" message along with the user's disabled comments.
    /// If the user is found and is active, it checks if the user's email is confirmed. If it's not, it returns an "Email is not verified" message.
    /// If the user's email is confirmed, it attempts to sign in the user with the provided password. If the sign-in fails, it returns an "Invalid Password" message.
    /// If the sign-in succeeds, it signs in the user and generates a JWT for the user. It also generates a list of abilities for the user based on their role.
    /// Finally, it returns a success message along with the user's details and the generated JWT.
    /// </remarks>
    public async Task<Response<LoginRes>> Login(LoginReq loginReq)
    {
        ApplicationUser? user = Helpers.EmailRegex().IsMatch(loginReq.Username)
        ? await _userManager.FindByEmailAsync(loginReq.Username)
        : await _userManager.FindByNameAsync(loginReq.Username);

        if (user == null)
        {
            return new Response<LoginRes>
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                StatusMessage = "User not found"
            };
        }

        //var result = await _userManager.CheckPasswordAsync(user, loginReq.Password);

        var result = await _signInManager.PasswordSignInAsync(loginReq.Username, loginReq.Password,
        true, false);

        if (!result.Succeeded)
        {
            return new Response<LoginRes>
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                StatusMessage = "Invalid Password"
            };
        }

        //if (user.IsActive == false)
        //{
        //    throw new Exception($"User does not active");
        //}

        // checking email is confirmed or not
        // var isConfirm = await UserManager.IsEmailConfirmedAsync(user);
        // if (!isConfirm)
        // {
        //     return user.BadRequest("Email is not verified");
        // }
        //var roles = await _userManager.GetRolesAsync(user);
        var context = UnitOfWork._context;
        //var appRoles = await UnitOfWork._context.Roles.Where(f => roles.Contains(f.Name)).ToListAsync();

        var roleClaims = await (from ur in context.UserRoles
                                join r in context.Roles on ur.RoleId equals r.Id
                                join c in context.RoleClaims on r.Id equals c.RoleId
                                where ur.UserId == user.Id
                                select new { claims = c, roles = r }).ToListAsync();



        //var appRoleIds = appRoles.Select(f => f.Id).ToList();


        //var roleClaims = await UnitOfWork._context.AppRoleClaims.Where(f => appRoleIds.Contains(f.RoleId)).ToListAsync();

        List<Claim> claims = new List<Claim>();

        claims.AddRange(roleClaims
                .SelectMany(f => f.claims.ClaimValue!.Split(",")
                    .Select(ff => new Claim(f.claims.ClaimType!, ff.Trim()))));

        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Name, user.UserName.ToString()));
        claims.Add(new Claim(ClaimTypes.Email, user.Email.ToString()));
        claims.Add(new Claim(ClaimTypes.Role, string.Join(",", roleClaims.Select(x => x.roles))));
        var token = GenerateToken(claims);




        LoginRes res = new LoginRes
        {
            Token = token,
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Roles = string.Join(",", roleClaims.Select(x => x.roles).Select(x => x.Name).Distinct().ToList()),
            Responsibilities = roleClaims.ToDictionary(x => x.claims.ClaimType, y => y.claims.ClaimValue),
        };


        return new Response<LoginRes>
        {
            Data = res,
            StatusCode = System.Net.HttpStatusCode.OK,
            StatusMessage = "Created successfully"
        };
    }

    public async Task<Response<LoginRes>> Register(SignUpReq registerReq)
    {
        var trans = await UnitOfWork.BeginTransactionAsync();
        try
        {
            var user = await _userManager.FindByEmailAsync(registerReq.Email)
                       ?? await _userManager.FindByNameAsync(registerReq.UserName);

            if (user != null)
            {
                return new Response<LoginRes>
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    StatusMessage = "A User with same Username or Email Already Exists!"
                };
            }


            user = new ApplicationUser
            {
                UserName = registerReq.UserName,
                Email = registerReq.Email,
                FirstName = registerReq.FirstName,
                LastName = registerReq.LastName,
                EmailConfirmed = true, // Should Remove it when Verifying via Email!
                IsActive = true,
                IsDeleted = false,
                MiddleName = registerReq.MiddleName,
                NormalizedEmail = registerReq.Email.ToUpper(),
                NormalizedUserName = registerReq.UserName.ToUpper(),
            };

            var userRes = await _userManager.CreateAsync(user, registerReq.Password);
            if (!userRes.Succeeded)
            {
                throw new Exception(string.Join(" ", userRes.Errors.Select(f => f.Description)));
            }

            await UnitOfWork.SaveAsync();

            await _signInManager.SignInAsync(user, true, null);
            await _userManager.AddToRoleAsync(user, Constants.ORGANIZATIONADMIN);
            await UnitOfWork.CommitTransactionAsync(trans);
            var context = UnitOfWork._context;
            var roleClaims = await (from ur in context.UserRoles
                                    join r in context.Roles on ur.RoleId equals r.Id
                                    join c in context.RoleClaims on r.Id equals c.RoleId
                                    where ur.UserId == user.Id
                                    select new { claims = c, roles = r }).ToListAsync();

            //var appRoleIds = appRoles.Select(f => f.Id).ToList();


            //var roleClaims = await UnitOfWork._context.AppRoleClaims.Where(f => appRoleIds.Contains(f.RoleId)).ToListAsync();

            List<Claim> claims = new List<Claim>();

            claims.AddRange(roleClaims
                    .SelectMany(f => f.claims.ClaimValue!.Split(",")
                        .Select(ff => new Claim(f.claims.ClaimType!, ff.Trim()))));
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim("Name", user.UserName.ToString()));
            claims.Add(new Claim("Email", user.Email.ToString()));
            claims.Add(new Claim("Role", string.Join(",", roleClaims.Select(x => x.roles))));

            var token = GenerateToken(claims);

            LoginRes res = new LoginRes
            {
                Token = token,
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = string.Join(",", roleClaims.Select(x => x.roles).Select(x => x.Name).Distinct().ToList()),
                Responsibilities = roleClaims.ToDictionary(x => x.claims.ClaimType, y => y.claims.ClaimValue),
            };

            return new Response<LoginRes>
            {
                Data = res,
                StatusCode = System.Net.HttpStatusCode.Created,
                StatusMessage = "Created successfully"
            };
        }
        catch (Exception e)
        {
            await UnitOfWork.RollBackTransactionAsync();
            return new Response<LoginRes>
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                StatusMessage = e.Message
            };
        }
    }

    public async Task<Response<ApplicationUserRes>> CreateWithRole(ApplicationUserReq userReq)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(userReq.Email)
                       ?? await _userManager.FindByNameAsync(userReq.UserName);

            if (user != null)
            {
                return new Response<ApplicationUserRes>
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    StatusMessage = "A User with same Username or Email Already Exists!"
                };
            }

            // await using var trans = await UnitOfWork.BeginTransactionAsync();

            user = new ApplicationUser
            {
                UserName = userReq.UserName,
                Email = userReq.Email,
                FirstName = userReq.FirstName,
                LastName = userReq.LastName,
                EmailConfirmed = true, // Should Remove it when Verifying via Email!
                IsActive = true,
                IsDeleted = false,
                MiddleName = userReq.MiddleName,
                NormalizedEmail = userReq.Email.ToUpper(),
                NormalizedUserName = userReq.UserName.ToUpper(),
            };

            var userRes = await _userManager.CreateAsync(user, userReq.UserName + "S@123");
            if (!userRes.Succeeded)
            {
                throw new Exception(string.Join(" ", userRes.Errors.Select(f => f.Description)));
            }

            await UnitOfWork.SaveAsync();

            await _userManager.AddToRoleAsync(user, userReq.Role);
            //await UnitOfWork.CommitTransactionAsync(trans);
            var res = user.Adapt<ApplicationUserRes>();

            return new Response<ApplicationUserRes>
            {
                Data = res,
                StatusCode = System.Net.HttpStatusCode.Created,
                StatusMessage = "Created successfully"
            };
        }
        catch (Exception e)
        {
            //await UnitOfWork.RollBackTransactionAsync();
            return new Response<ApplicationUserRes>
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                StatusMessage = e.Message
            };
        }
    }

    public async Task<Response<ApplicationUserRes>> UpdateApplicationUser(ApplicationUserReq userReq)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userReq.Id.ToString());

            if (user == null)
            {
                return new Response<ApplicationUserRes>
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    StatusMessage = "ApplicationUser not Exists!"
                };
            }


            var existing = await _userManager.FindByEmailAsync(userReq.Email)
                       ?? await _userManager.FindByNameAsync(userReq.UserName);

            if (existing != null && existing.Id != user.Id)
            {
                return new Response<ApplicationUserRes>
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    StatusMessage = "A User with same Username or Email Already Exists!"
                };
            }

            // await using var trans = await UnitOfWork.BeginTransactionAsync();

            user.UserName = userReq.UserName;
            user.Email = userReq.Email;
            user.FirstName = userReq.FirstName;
            user.LastName = userReq.LastName;
            user.MiddleName = userReq.MiddleName;
            user.NormalizedEmail = userReq.Email.ToUpper();
            user.NormalizedUserName = userReq.UserName.ToUpper();


            var userRes = await _userManager.UpdateAsync(user);
            if (!userRes.Succeeded)
            {
                throw new Exception(string.Join(" ", userRes.Errors.Select(f => f.Description)));
            }

            await UnitOfWork.SaveAsync();

            //await UnitOfWork.CommitTransactionAsync(trans);
            var res = user.Adapt<ApplicationUserRes>();

            return new Response<ApplicationUserRes>
            {
                Data = res,
                StatusCode = System.Net.HttpStatusCode.Created,
                StatusMessage = "Updated successfully"
            };
        }
        catch (Exception e)
        {
            //await UnitOfWork.RollBackTransactionAsync();
            return new Response<ApplicationUserRes>
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                StatusMessage = e.Message
            };
        }
    }

    //public async Task<IActionResult> ConfirmEmail(string email, string token)
    //{
    //    try
    //    {
    //        var user = !string.IsNullOrEmpty(email) ? await _userManager.FindByEmailAsync(email) : null;
    //        if (user == null)
    //        {
    //            return new BadRequestObjectResult(new { status = 400, message = "Invalid Email" });
    //        }

    //        var result = await _userManager.ConfirmEmailAsync(user, token);
    //        if (!result.Succeeded)
    //        {
    //            return new BadRequestObjectResult(new
    //            { status = 400, message = "Can not Verify Email at this time." });
    //        }

    //        await _signInManager.SignInAsync(user, new AuthenticationProperties());
    //        var accessToken = user.GenerateJwt(_configuration);
    //        var userData = new
    //        {
    //            Name = $"{user.FirstName} {user.LastName}",
    //            user.Role,
    //            user.UserName,
    //            user.Email,
    //            user.ProfilePic,
    //            user.EmailConfirmed,
    //            UserId = user.Id,
    //        };
    //        return userData.Ok("User Login Successful!", new { AccessToken = accessToken });
    //    }
    //    catch (Exception ex)
    //    {
    //        return ex.BadRequest();
    //    }
    //}

    //public async Task<IActionResult> ResendVerificationEmail()
    //{
    //    var userId = Context.GetUserId();
    //    var user = await _userManager.Users.FirstOrDefaultAsync(f => f.Id == userId);
    //    if (user == null)
    //    {
    //        return user.NotFound("User Not Found!");
    //    }

    //    var html = await EmailConfirmationTemplate(user);
    //    await _emailSender.SendEmailAsync(user.Email, "Verify Your Email!", html);

    //    return "Email Send!".Ok();
    //}

    //private async Task<string> EmailConfirmationTemplate(ApplicationUser user)
    //{
    //    // sending mail for email confirmation
    //    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
    //    token = token.Replace("+", "-");
    //    var confirmEmailPageLink =
    //        $"{_configuration["Urls:WebAppBaseUrl"] + _configuration["Urls:WebAppEmailConfirmationEndPoint"]}?email={user.Email}&token={token}";
    //    var userName = user.FirstName.ToUpper() + " " + user.LastName!.ToUpper();
    //    var baseTemplate = _emailSender.ReadBaseTemplate();
    //    var emailVerifyContent = _emailSender.ReadVerifyEmailContent();
    //    // var availableProperties = _emailSender.ReadAvailableProperties(baseTemplate, emailVerifyContent);
    //    var ourKeyVal = _emailSender.KeyValueBindings;
    //    foreach (var emailSenderKeyValueBinding in ourKeyVal)
    //    {
    //        ourKeyVal[emailSenderKeyValueBinding.Key] = emailSenderKeyValueBinding.Value switch
    //        {
    //            "EMAIL_CONTENT" => emailVerifyContent ?? string.Empty,
    //            "WEBSITE_URL" => _configuration["Urls:WebAppBaseUrl"] ?? string.Empty,
    //            "USER_NAME" => userName ?? string.Empty,
    //            "SUPPORT_EMAIL" => _configuration["EmailSender:UserName"] ?? string.Empty,
    //            "SOCIAL_FB_ID" => _configuration["Urls:Social_Facebook_Id"] ?? string.Empty,
    //            "SOCIAL_TI_ID" => _configuration["Urls:Social_Twitter_Id"] ?? string.Empty,
    //            "SOCIAL_IG_ID" => _configuration["Urls:Social_Instagram_Id"] ?? string.Empty,
    //            "TOU_URL" => _configuration["Urls:TermOfUse"] ?? string.Empty,
    //            "PP_URL" => _configuration["Urls:PrivacyPolicy"] ?? string.Empty,
    //            "VERIFICATION_URL" => confirmEmailPageLink ?? string.Empty,
    //            _ => ourKeyVal[emailSenderKeyValueBinding.Key]
    //        };
    //    }

    //    var finalHtml = $"{baseTemplate}";
    //    foreach (var (key, value) in ourKeyVal)
    //    {
    //        finalHtml = finalHtml.Replace(key, value);
    //    }

    //    return finalHtml;
    //}

    private string GenerateToken(IEnumerable<Claim> claims,
        string? issuer = null,
        string? audience = null)
    {
        var authSigningKey =
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secrets.AuthenticationSchemeSecretKey));
        var tokenExpiryTimeInHour = Convert.ToInt64(1);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer ?? Secrets.AuthenticationSchemeIssuer,
            Audience = audience ?? Secrets.AuthenticationSchemeAudience,
            Expires = DateTime.UtcNow.AddHours(tokenExpiryTimeInHour),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}