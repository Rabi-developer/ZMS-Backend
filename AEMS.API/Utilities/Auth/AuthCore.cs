using IMS.Business.Utitlity;
using IMS.Domain.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IMS.API.Utilities.Auth;

public static class AuthCore
{
    public static string UpdateJwtToken(this HttpContext httpContext, string? issuer = null,
        string? audience = null)
    {
        var authToken = httpContext.Request.Headers["Authorization"].ToString();
        authToken = authToken.Replace("Bearer", "", true, CultureInfo.InvariantCulture).Trim();
        if (string.IsNullOrEmpty(authToken))
        {
            return "";
        }

        var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(authToken);
        if (jwtSecurityToken == null)
        {
            return "";
        }

        var authSigningKey =
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secrets.AuthenticationSchemeSecretKey));
        var tokenExpiryTimeInHour = Convert.ToInt64(1);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer ?? Secrets.AuthenticationSchemeIssuer,
            Audience = audience ?? Secrets.AuthenticationSchemeAudience,
            Subject = new ClaimsIdentity(jwtSecurityToken.Claims),
            Expires = DateTime.Now.AddHours(tokenExpiryTimeInHour),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static string GenerateToken(IEnumerable<Claim> claims,
        string? issuer = null,
        string? audience = null)
    {
        var authSigningKey =
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secrets.AuthenticationSchemeSecretKey));
        var tokenExpiryTimeInHour = Convert.ToInt64(9);
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

    public static AuthenticationBuilder AddAEMSAuthentication(this IServiceCollection services,
        string? issuer = null, string? audience = null)
    {
        return services.AddLogging(builder => builder.AddConsole()).AddAuthentication("Bearer").AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = issuer ?? Secrets.AuthenticationSchemeIssuer,
                ValidateAudience = true,
                ValidAudience = audience ?? Secrets.AuthenticationSchemeAudience,
                ValidateIssuerSigningKey = true,
                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secrets.AuthenticationSchemeSecretKey))
            };
        });
    }

    public static IServiceCollection AddAEMSAuthorization(this IServiceCollection services)
    {
        return services.AddAuthorization(options =>
        {
            var policyBuilder = new AuthorizationPolicyBuilder();
            var defaultPolicy = policyBuilder
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
            options.DefaultPolicy = defaultPolicy;
            options.AddPolicy("SuperAdmin",
            policy =>
            {
                policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(ClaimStore.ResourceClaim(ClaimStore.Resources.All), ClaimStore.Actions.All);
            });
            foreach (var resource in ClaimStore.Resources.List)
            {
                foreach (var action in ClaimStore.Actions.All)
                {
                    options.AddPolicy($"{action}{resource.ResourceName}",
                        policy =>
                        {
                            policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                            policy.RequireAuthenticatedUser();
                            policy.RequireClaim(ClaimStore.ResourceClaim(resource.ResourceName), action);
                        });
                }

                options.AddPolicy($"Manage{resource.ResourceName}",
                    policy =>
                    {
                        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim(ClaimStore.ResourceClaim(resource.ResourceName),
                            ClaimStore.Actions.Manage);
                    });
                options.AddPolicy($"All{resource.ResourceName}",
                    policy =>
                    {
                        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim(ClaimStore.ResourceClaim(resource.ResourceName),
                            ClaimStore.Actions.All);
                    });
            }
        });
    }
}

