using IMS.Domain.Base;

namespace IMS.Domain.Entities;

public class Level : GeneralBase
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
    public Level? Parent { get; set; }
    public LevelType Type { get; set; }
    public Guid? OrganizationId { get; set; }
    public Organization? Organization { get; set; }
}
