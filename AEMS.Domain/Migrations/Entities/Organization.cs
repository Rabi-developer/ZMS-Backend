using IMS.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace IMS.Domain.Entities;

public class Organization : GeneralBase
{
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Website { get; set; }
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string? Zip { get; set; }
    public OrganizationSetting? Setting { get; set; }
    public List<Address>? Addresses { get; set; }
    public List<Branch>? Branches { get; set; }
    public List<OrganizationUser>? OrganizationUsers { get; set; }
}
