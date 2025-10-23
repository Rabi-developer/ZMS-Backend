using IMS.API.Utilities.Auth;
using IMS.Business.Services;
using IMS.Domain.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ZMS.API.Middleware
{
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IRoleService roleService)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata.GetMetadata<PermissionAttribute>() is { } permission)
            {
                // SuperAdmin bypass
                if (context.User.HasAnyRole("SuperAdmin"))
                {
                    await _next(context);
                    return;
                }

                if (!context.User.Identity.IsAuthenticated)
                {
                    context.Response.StatusCode = 401;
                    return;
                }

                var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
                {
                    context.Response.StatusCode = 401;
                    return;
                }

                var entityType = GetEntityTypeFromUrl(context.Request.Path) ?? permission.Resource;

                var hasPermission = await roleService.UserHasPermission(
                    userId.ToString(),
                    entityType,
                    permission.Action);

                if (!hasPermission)
                {
                    context.Response.StatusCode = 403;
                    return;
                }
            }

            await _next(context);
        }

        private string GetEntityTypeFromUrl(PathString path)
        {
            // Example logic to extract an entity type from the URL path
            // You can adjust this based on your API structure
            var segments = path.Value.Split('/');

            if (segments.Length >= 2 && segments[1].Length > 0)
            {
                return segments[2]; // Assuming the second segment is the entity (e.g., "Stock")
            }

            return null; // Default to null or another default behavior
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class PermissionAttribute : Attribute
    {
        public string Resource { get; }
        public string Action { get; }

        public PermissionAttribute(string resource, string action)
        {
            Resource = resource;
            Action = action;
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class EntityTypeAttribute : Attribute
    {
        public string EntityType { get; }

        public EntityTypeAttribute(Type entityType)
        {
            EntityType = entityType.Name;
        }
    }


    public static class PermissionMiddlewareExtensions
    {
        public static IApplicationBuilder UsePermissionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PermissionMiddleware>();
        }
    }

}