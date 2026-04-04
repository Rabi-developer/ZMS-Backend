using System.ComponentModel.DataAnnotations;

namespace IMS.Business.DTOs.Requests;

public class OrganizationReq
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? Zip { get; set; }
}

public class OrganizationUpdateReq : OrganizationReq
{
    public Guid Id { get; set; }
}