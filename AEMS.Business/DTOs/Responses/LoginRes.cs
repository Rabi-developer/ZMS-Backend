namespace IMS.Business.DTOs.Responses;

public class LoginRes
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public List<string> Roles { get; set; }
    public string Token { get; set; }
    public Dictionary<string, List<string>> Permissions { get; set; }
}
