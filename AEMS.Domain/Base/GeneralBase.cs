using System.ComponentModel.DataAnnotations;

namespace IMS.Domain.Base;

public interface IMinBase
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public Guid CreatedBy { get; set; }
}

public interface IGeneralBase : IMinBase
{
    public DateTime? ModifiedDateTime { get; set; }
    public Guid? ModifiedBy { get; set; }
}

public class MinBase : IMinBase
{
    [Key]
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public Guid CreatedBy { get; set; }
}

public class GeneralBase : IGeneralBase
{
    [Key]
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public Guid CreatedBy { get; set; } 
    public DateTime? ModifiedDateTime { get; set; }
    public Guid? ModifiedBy { get; set; }
}

