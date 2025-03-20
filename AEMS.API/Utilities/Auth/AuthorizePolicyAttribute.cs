using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IMS.API.Utilities.Auth;

public class AuthorizePolicyAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    protected string[] PolicyNames;
    protected bool IsEnvironmentCheck = false;
    //private const string HeaderName = Helpers.EnvironmentId;
    private const string LowerEnvironmentId = "environment-id";

    public AuthorizePolicyAttribute(params string[] policyNames)
    {
        PolicyNames = policyNames;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Check whether is environment header set.
        //if (IsEnvironmentCheck && !context.HttpContext.Request.Headers.TryGetValue(HeaderName, out _)
        //    && !context.HttpContext.Request.Headers.TryGetValue(LowerEnvironmentId, out _))
        //{
        //    // User is not authenticated
        //    context.Result = new BadRequestObjectResult(new { message = "Please select an Environment first" });
        //}

        if (context.HttpContext.User.Identity?.IsAuthenticated == true)
        {
            // User is authenticated
            foreach (var policyName in PolicyNames)
            {
                var authorizationService = context.HttpContext.RequestServices.GetService<IAuthorizationService>();
                if (authorizationService == null) continue;
                var authorizationResult =
                    authorizationService.AuthorizeAsync(context.HttpContext.User, policyName).Result;

                if (authorizationResult.Succeeded)
                {
                    return; // User is authorized by at least one policy
                }
            }

            // User is not authorized by any of the policies
            context.Result = new BadRequestObjectResult(new { message = "You don't have access to this resource." });
        }
        else
        {
            // User is not authenticated
            context.Result = new BadRequestObjectResult(new { message = "Your are no longer Authenticated. Kindly login again." });
        }
    }
}

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
public class AuthorizeAnyPolicyAttribute : AuthorizePolicyAttribute
{
    public AuthorizeAnyPolicyAttribute(params string[] policyNames)
    {
        PolicyNames = policyNames;
    }
}
