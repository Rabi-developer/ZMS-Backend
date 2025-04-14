using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Domain.Entities;

public class OrganizationSetting
{
    public Guid? LogoId { get; set; }
    public Attachment? Logo { get; set; }
    [Key]
    public Guid OrganizationId { get; set; }
    [ForeignKey(nameof(OrganizationId))]
    public Organization Organization { get; set; }
}