using IMS.Business.Utitlity;
using IMS.Domain.Base;
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

namespace ZMS.API.Utilities.Auth;

public static class AuthCore
{
    private const int DefaultTokenExpiryHours = 1;
    private static readonly TimeSpan ClockSkew = TimeSpan.FromMinutes(1);

    /// <summary>
    /// Updates an existing JWT token with extended expiration
    /// </summary>
    public static string UpdateJwtToken(this HttpContext httpContext, string? issuer = null, string? audience = null)
    {
        var authToken = httpContext.GetBearerToken();
        if (string.IsNullOrEmpty(authToken))
        {
            return string.Empty;
        }

        var jwtSecurityToken = ReadJwtToken(authToken);
        if (jwtSecurityToken == null)
        {
            return string.Empty;
        }

        var tokenDescriptor = CreateTokenDescriptor(
            jwtSecurityToken.Claims,
            issuer,
            audience,
            DateTime.UtcNow.AddHours(DefaultTokenExpiryHours));

        return GenerateToken(tokenDescriptor);
    }

    /// <summary>
    /// Generates a new JWT token with the specified claims
    /// </summary>
    public static string GenerateToken(IEnumerable<Claim> claims, string? issuer = null, string? audience = null)
    {
        var tokenDescriptor = CreateTokenDescriptor(
            claims,
            issuer,
            audience,
            DateTime.UtcNow.AddHours(DefaultTokenExpiryHours));

        return GenerateToken(tokenDescriptor);
    }

    /// <summary>
    /// Configures JWT Bearer authentication
    /// </summary>
    public static AuthenticationBuilder AddZMSAuthentication(this IServiceCollection services,
     string? issuer = null,
     string? audience = null)
    {
        return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer ?? Secrets.AuthenticationSchemeIssuer,
                    ValidateAudience = true,
                    ValidAudience = audience ?? Secrets.AuthenticationSchemeAudience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Secrets.AuthenticationSchemeSecretKey)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception is SecurityTokenInvalidSigningKeyException)
                        {
                            context.Response.Headers.Append("Invalid-Signing-Key", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
    }

    /// <summary>
    /// Configures authorization policies
    /// </summary>
    public static IServiceCollection AddZMSAuthorization(this IServiceCollection services)
    {
        return services.AddAuthorization(options =>
        {

            // Default policy
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();

            // SuperAdmin policy
            options.AddPolicy("SuperAdmin", policy =>
            {
                policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(Claimstore.ResourceClaim(Claimstore.Resources.All), Claimstore.Actions.All);
            });

            // Dynamic policies for each resource and action
            foreach (var resource in Claimstore.Resources.List)
            {
                // Individual action policies (Create, Read, Update, Delete)
                foreach (var action in Claimstore.Actions.All)
                {
                    options.AddPolicy($"{action}{resource.ResourceName}", policy =>
                    {
                        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim(Claimstore.ResourceClaim(resource.ResourceName), action);
                    });
                }

                // Manage policy (all actions except All)
                options.AddPolicy($"Manage{resource.ResourceName}", policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(Claimstore.ResourceClaim(resource.ResourceName),
                        Claimstore.Actions.Manage);
                });

                // All actions policy
                options.AddPolicy($"All{resource.ResourceName}", policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(Claimstore.ResourceClaim(resource.ResourceName),
                        Claimstore.Actions.All);
                });
            }
        });
    }

    #region Private Helper Methods

    private static string GetBearerToken(this HttpContext httpContext)
    {
        var authToken = httpContext.Request.Headers["Authorization"].ToString();
        return authToken.Replace("Bearer", "", true, CultureInfo.InvariantCulture).Trim();
    }

    private static JwtSecurityToken? ReadJwtToken(string token)
    {
        try
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token);
        }
        catch
        {
            return null;
        }
    }

    private static SecurityTokenDescriptor CreateTokenDescriptor(
        IEnumerable<Claim> claims,
        string? issuer,
        string? audience,
        DateTime expires)
    {
        return new SecurityTokenDescriptor
        {
            Issuer = issuer ?? Secrets.AuthenticationSchemeIssuer,
            Audience = audience ?? Secrets.AuthenticationSchemeAudience,
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secrets.AuthenticationSchemeSecretKey)),
                SecurityAlgorithms.HmacSha256)
        };
    }

    private static string GenerateToken(SecurityTokenDescriptor tokenDescriptor)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private static TokenValidationParameters CreateTokenValidationParameters(string? issuer, string? audience)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer ?? Secrets.AuthenticationSchemeIssuer,
            ValidateAudience = true,
            ValidAudience = audience ?? Secrets.AuthenticationSchemeAudience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(Secrets.AuthenticationSchemeSecretKey)),
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = ClockSkew,

            // Additional security best practices
            NameClaimType = ClaimTypes.Name,
            RoleClaimType = ClaimTypes.Role
        };
    }

    #endregion
}