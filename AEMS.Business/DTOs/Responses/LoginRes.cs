namespace IMS.Business.DTOs.Responses;

public class LoginRes
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Roles { get; set; } //test
    public string Token { get; set; }
    public Dictionary<string, string> Responsibilities { get; set; }
}
