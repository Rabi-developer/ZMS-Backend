using System.ComponentModel.DataAnnotations;

namespace IMS.Business.DTOs.Requests;

public class ApplicationUserReq
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? Password { get; set; }
}

public class CreateUserWithRoleReq : ApplicationUserReq
{
    public string Role { get; set; }
}

public class UpdateUserReq : ApplicationUserReq
{
    public Guid Id { get; set; }
}
