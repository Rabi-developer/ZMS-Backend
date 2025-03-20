using IMS.Domain.Base;
using IMS.Domain.Entities;

namespace IMS.Business.DTOs.Responses;

public class LevelRes
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid ParentId { get; set; }
    public Level Parent { get; set; }
    public LevelType Type { get; set; }
    public string ContactPerson { get; set; }
    public Guid? AddressId { get; set; }
    public Guid OrganizationId { get; set; }
}
