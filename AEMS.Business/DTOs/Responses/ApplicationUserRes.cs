using System.ComponentModel.DataAnnotations;

namespace IMS.Business.DTOs.Responses;

public class ApplicationUserRes
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    //public string Phone { get; set; }
    public string UserName { get; set; }
    public string Role { get; set; }

}
