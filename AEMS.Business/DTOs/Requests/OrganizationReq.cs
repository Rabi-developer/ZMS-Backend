using System.ComponentModel.DataAnnotations;

namespace IMS.Business.DTOs.Requests;

public class OrganizationReq
{
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Url]
    public string Website { get; set; }
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string? Zip { get; set; }
}

public class OrganizationUpdateReq : OrganizationReq
{
    public Guid Id { get; set; }
}