namespace IMS.DataAccess;

public static class Helpers
{
    //public static Guid GetUserId(this HttpContext httpContext)
    //{
    //    var s = httpContext.User.Claims
    //        .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    //    return s == null ? Guid.Empty : Guid.Parse(s);
    //}

    //public static (string, Guid) GetRoleAndId(this HttpContext context)
    //{
    //    var roleClaim = context.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType);
    //    var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
    //    if (roleClaim != null && userIdClaim != null)
    //    {
    //        return (roleClaim.Value, Guid.Parse(userIdClaim.Value));
    //    }

    //    return ("", Guid.Empty);
    //}
}
