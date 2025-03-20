using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Domain.Entities;

public class OrganizationUser
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; }
}