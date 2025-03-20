using System.ComponentModel.DataAnnotations;

namespace IMS.Business.DTOs.Requests;

public class SignUpReq
{
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    //public string Phone { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}

