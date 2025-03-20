namespace IMS.Domain.Base;

public static class ClaimStore
{
    //#region AuthModule
    //public const string RefreshToken = "RefreshToken";
    //public const string ForgotPasswordToken = "ForgotPasswordToken";
    //public const string ActivateAccountToken = "ActivateAccountToken";
    //#endregion

    //#region RoleManagement
    //public const string ManageRole = "ManageRole";
    //public const string CreateRole = "CreateRole";
    //public const string ViewRole = "ViewRole";
    //#endregion

    //#region UserManagement
    //public const string ManageUser = "ManageUser";
    //public const string CreateUser = "CreateUser";
    //public const string ViewUser = "ViewUser";
    //#endregion

    //public static Dictionary<string, List<string>> GetClaims()
    //{
    //    return new Dictionary<string, List<string>>()
    //    {
    //        { "AuthManagement", new List<string> { RefreshToken , ForgotPasswordToken ,ActivateAccountToken } },
    //        { "RoleManagement", new List<string> {  ViewRole ,  ManageRole } },
    //        { "UserManagement", new List<string> { ManageUser, ViewUser } },
    //    };
    //}

    public const string AuthorizationClaim = "Authorization";
    public const string UserId = "UserId";
    public const string AccessLevelClaim = "AccessLevel";
    public const string AccessLevelDetailsClaim = "AccessLevelDetails";
    public const string ResourceClaimPrefix = "Resource_";

    public static string ResourceClaim(string resource)
    {
        return ResourceClaimPrefix + resource;
    }

    public static class Actions
    {
        public static string Create = "Create";
        public static string Read = "Read";
        public static string Update = "Update";
        public static string Delete = "Delete";
        public static string Execute = "Execute";

        public static string[] All = new[] { Create, Read, Update, Delete, Execute };
        public static string[] Manage = new[] { Create, Read, Update, Delete };
    }

    public static class AccessLevel
    {
        public static string All = "All";
        public static string Self = "Self";
        public static string User = "User:{0}";

        public static string Organization = "Organization:{0}";

        // AccessLevel: Self;User:{48D66AC6-975E-4A6A-B00A-12F41F7FD500},{48D66AC6-975E-4A6A-B00A-12F41F7FD500}
        public static string SpecificUsers(params string[] userIds)
        {
            return $"User:{string.Join(',', userIds.Select(f => "{" + f + "}"))}";
        }

        public static string SpecificUsers(params Guid[] userIds)
        {
            return $"User:{string.Join(',', userIds.Select(f => "{" + f + "}"))}";
        }

        public static string SpecificOrganizations(params string[] organizationIds)
        {
            return $"Organization:{string.Join(',', organizationIds.Select(f => "{" + f + "}"))}";
        }

        public static string SpecificOrganizations(params Guid[] organizationIds)
        {
            return $"Organization:{string.Join(',', organizationIds.Select(f => "{" + f + "}"))}";
        }
    }

    public static class Resources
    {
        public static string All = "All";
        public static string Home = "Home";
        public static string ProfileConfiguration = "ProfileConfiguration";

        public static string Organization = "Organization";
        public static string OrganizationUser = "OrganizationUser";
        public static string Branch = "Branch";
        public static string Department = "Department";
    
        public static string Section = "Section";
        public static string Address = "Address";
  
        public static string UsersManagement = "UsersManagement";
  
        public static string Role = "Role";
        

        public static IList<ResPerm> List = new List<ResPerm>
        {
            new()
            {
                ResourceName = All,

            },
             new()
            {
                ResourceName = ProfileConfiguration,

            },
            new()
            {
                ResourceName = Branch,

            },
            new()
            {
                ResourceName = Organization,

            },
            new()
            {
                ResourceName = OrganizationUser,

            },
           
            new()
            {
                ResourceName = UsersManagement,

            },
           
          
            new()
            {
                ResourceName = Role,

            },
           
            new()
            {
                ResourceName = Home,

            },
           
            new()
            {
            ResourceName = Department,
            },
           
            new ()
            {
            ResourceName = Section,
            },
            new ()
            {
            ResourceName = Address ,
            },
           
        };

        public static string SpecificResources(string resource, params string[] resourceIds)
        {
            return $"{resource}:{string.Join(',', resourceIds.Select(f => "{" + f + "}"))}";
        }

        public static string SpecificResources(string resource, params Guid[] resourceIds)
        {
            return $"{resource}:{string.Join(',', resourceIds.Select(f => "{" + f + "}"))}";
        }
    }

    //string,string,string[] => accessLevels,resources,actions
    public static string GenerateAccessClaim(params (string, string, string[])[] resourceActions)
    {
        var resourceClaim = string.Join("&",
            resourceActions.Select(tuple => $"[{tuple.Item1};{tuple.Item2};{string.Join("|", tuple.Item3)}]"));
        return resourceClaim;
    }

    public static (string, string, string[])[] ExtractResourceClaim(string resourceClaim)
    {
        var s = resourceClaim.Split("&")
            .Select(f => (f.Split(";")[0].TrimStart('['), f.Split(";")[1], f.Split(";")[2].TrimEnd(']').Split('|')))
            .ToArray();
        return s;
    }

    public class BaseRoleReq
    {
        // <role,claims>
        // claims ->  <resource, action, accessLevel>
        public IDictionary<string, string[]> Claims = new Dictionary<string, string[]>();
    }
}
