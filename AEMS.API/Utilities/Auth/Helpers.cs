using System.Security.Claims;
//using ClaimTypes = AEMS.Domain.Base.ClaimTypes;


namespace IMS.API.Utilities.Auth;   

public static class Helpers
{
    public const string EnvironmentId = "Environment-Id";
    public const string LowerEnvironmentId = "environment-id";
    public const string GlobalEnvironmentId = "11111111-1111-1111-1111-111111111111";

    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            collection.Add(item);
        }
    }

    ///// <summary>
    ///// Retrieves a list of modules from the provided <see cref="IModuleFiltration"/> object.
    ///// </summary>
    ///// <param name="moduleFiltration">The object implementing <see cref="IModuleFiltration"/> to retrieve the modules from.</param>
    ///// <returns>A list of modules.</returns>
    //public static IList<string> GetModules(this IModuleFiltration moduleFiltration) => moduleFiltration.Modules
    //    .Split(",").Select((Func<string, string>)(f => f.Trim())).ToList();

    ///// <summary>
    ///// Sets the modules for module filtration.
    ///// </summary>
    ///// <param name="moduleFiltration">The module filtration object.</param>
    ///// <param name="modules">The modules to be set.</param>
    ///// <returns>
    ///// A string representing the updated modules after setting the provided modules.
    ///// </returns>
    //public static string SetModules(this IModuleFiltration moduleFiltration, params string[] modules)
    //{
    //    foreach (var module in modules)
    //    {
    //        if (moduleFiltration.GetModules().Contains(module)) continue;
    //        var exMods = moduleFiltration.GetModules();
    //        exMods.Add(module);
    //        moduleFiltration.Modules = string.Join(",", exMods);
    //    }

    //    return moduleFiltration.Modules.Trim().Trim(',');
    //}

    /// <summary>
    /// Returns the user ID for the specified ClaimsPrincipal.
    /// </summary>
    /// <param name="user">The ClaimsPrincipal object.</param>
    /// <returns>The user ID as a nullable Guid.</returns>
    public static Guid? GetUserId(this ClaimsPrincipal user)
    {
        Guid? uId = null;
        var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!string.IsNullOrEmpty(userIdString) && Guid.TryParse(userIdString, out var userId))
        {
            uId = userId;
        }

        return uId;
    }

    /// <summary>
    /// Returns the EnvironmentId  for the specified ClaimsPrincipal.
    /// </summary>
    /// <param name="user">The ClaimsPrincipal object.</param>
    /// <returns>The user ID as a nullable Guid.</returns>
    public static Guid GetEnvironmentId(this HttpContext context)
    {
        var environmentHeader = context.Request.Headers[EnvironmentId].FirstOrDefault();
        if (string.IsNullOrEmpty(environmentHeader))
        {
            environmentHeader = context.Request.Headers[LowerEnvironmentId].FirstOrDefault();
        }
        return !string.IsNullOrWhiteSpace(environmentHeader) && Guid.TryParse(environmentHeader, out Guid parsedGuid)
            ? parsedGuid
            : Guid.Empty;
    }

    /// <summary>
    /// Returns the true if envId is global  for the specified ClaimsPrincipal.
    /// </summary>
    /// <param name="user">The ClaimsPrincipal object.</param>
    /// <returns>The user ID as a nullable Guid.</returns>
    public static bool IsGlobalEnvironment(this HttpContext context)
    {
        Guid environmentId;
        var environmentHeader = context.Request.Headers[EnvironmentId].FirstOrDefault();
        if (string.IsNullOrEmpty(environmentHeader))
        {
            environmentHeader = context.Request.Headers[LowerEnvironmentId].FirstOrDefault();
        }
        environmentId =
            !string.IsNullOrWhiteSpace(environmentHeader) && Guid.TryParse(environmentHeader, out Guid parsedGuid)
                ? parsedGuid
                : Guid.Empty;
        return environmentId == Guid.Parse(GlobalEnvironmentId) ? true : false;
    }

    /// <summary>
    /// Returns the user Email for the specified ClaimsPrincipal.
    /// </summary>
    /// <param name="user">The ClaimsPrincipal object.</param>
    /// <returns>The user Email as a nullable string.</returns>
    public static string? GetUserEmail(this ClaimsPrincipal user)
    {
        string? email = null;
        var userEmail = user.FindFirstValue(ClaimTypes.Email);
        if (!string.IsNullOrEmpty(userEmail))
        {
            email = userEmail;
        }

        return email;
    }

    /// <summary>
    /// Retrieves the roles associated with a user.
    /// </summary>
    /// <param name="user">The user for whom roles need to be retrieved.</param>
    /// <returns>A list of roles associated with the user.</returns>
    public static List<string> GetRoles(this ClaimsPrincipal user)
    {
        var roles = new List<string>();
        var userIdString = user.FindFirstValue(ClaimTypes.Role);
        if (!string.IsNullOrEmpty(userIdString))
        {
            roles.AddRange(userIdString.Split(",").Select(f => f.Trim()));
        }

        return roles;
    }

    /// <summary>
    /// Checks if the given ClaimsPrincipal has any of the specified roles.
    /// </summary>
    /// <param name="user">The ClaimsPrincipal to check the roles for.</param>
    /// <param name="roles">The roles to check against.</param>
    /// <returns>True if the user has any of the specified roles, otherwise false.</returns>
    public static bool HasAnyRole(this ClaimsPrincipal user, params string[] roles)
    {
        var rolesLis = user.GetRoles();
        return roles.Any(role => rolesLis.Contains(role));
    }

    /// <summary>
    /// Generates a random password with a length of 10 characters. The password includes at least one uppercase letter, one number, and one symbol. The rest of the characters are randomly chosen from a set of lowercase letters, uppercase letters, numbers, and symbols. The order of the characters in the password is also randomized.
    /// </summary>
    /// <returns>A string representing the generated password.</returns>
    public static string GeneratePassword(int numberOfChars = 12)
    {
        const string lowercase = "abcdefghijklmnopqrstuvwxyz";
        const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string numbers = "1234567890";
        const string symbols = "!@#$%^&*()";
        // const string symbols = "";
        var combined = lowercase + uppercase + numbers + symbols[new Range(0, 2)];
        var rnd = new Random();
        var password = "";
        // Ensure the password contains at least one uppercase letter, one number, and one symbol
        password += uppercase[rnd.Next(uppercase.Length)];
        password += numbers[rnd.Next(numbers.Length)];
        password += symbols[rnd.Next(symbols.Length)];
        // Fill up the rest of the password length with random characters from the combined string
        for (var i = password.Length; i < numberOfChars; i++)
        {
            password += combined[rnd.Next(combined.Length)];
        }

        // Convert the password to an array and shuffle it to ensure the uppercase letter, number, and symbol are not always in the same position
        var passwordArray = password.ToCharArray();
        Array.Sort(passwordArray, (x, y) => rnd.Next(-1, 2));
        return new string(passwordArray);
    }

    /// <summary>
    /// Generates an OTP (One-Time Password) of a specified length.
    /// </summary>
    /// <param name="numberOfChars">The number of characters in the OTP.</param>
    /// <param name="isNumeric">A boolean value indicating whether the OTP should be numeric (true) or alphanumeric (false).</param>
    /// <param name="useUppercase">A boolean value indicating whether to use uppercase letters in the OTP. This parameter is ignored if isNumeric is true.</param>
    /// <param name="useLowercase">A boolean value indicating whether to use lowercase letters in the OTP. This parameter is ignored if isNumeric is true.</param>
    /// <returns>A string representing the generated OTP.</returns>
    public static string GenerateOtp(int numberOfChars = 6, bool isNumeric = true, bool useLowercase = true,
        bool useUppercase = false, bool ensureAllTypes = false)
    {
        const string numbers = "1234567890";
        var uppercase = useUppercase ? "ABCDEFGHIJKLMNOPQRSTUVWXYZ" : "";
        const string symbols = "!@#$%^&*()";
        var lowercase = useLowercase ? "abcdefghijklmnopqrstuvwxyz" : "";
        var characters = isNumeric ? numbers : symbols + lowercase + uppercase + numbers;
        var random = new Random();
        var otp = string.Empty;

        if (ensureAllTypes)
        {
            otp += lowercase[random.Next(lowercase.Length)];
            otp += uppercase[random.Next(uppercase.Length)];
            otp += numbers[random.Next(numbers.Length)];
            otp += symbols[random.Next(symbols.Length)];
        }

        for (var i = 0; i < numberOfChars; i++)
        {
            otp += characters[random.Next(characters.Length)];
        }

        return otp;
    }
}
