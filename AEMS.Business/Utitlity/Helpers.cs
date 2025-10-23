using System.Security.Claims;
using System.Text.RegularExpressions;


namespace IMS.Business.Utitlity;

public static partial class Helpers
{
    [GeneratedRegex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
    public static partial Regex EmailRegex();

    public static Guid? GetUserIdN(this ClaimsPrincipal user)
    {
        Guid? uId = null;
        var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!string.IsNullOrEmpty(userIdString) && Guid.TryParse(userIdString, out var userId))
        {
            uId = userId;
        }

        return uId;
    }
}
