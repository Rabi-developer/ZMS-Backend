using IMS.Domain.Base;
using IMS.Domain.Base;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>, IMinBase
{
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public Guid? AddressId { get; set; }
    [ForeignKey("AddressId")]
    public Address? Address { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? ProfilePictureId { get; set; }
    public Attachment? ProfilePicture { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? UserId { get; set; }
    public string? Files { get; set; }
}

public class AppRole : IdentityRole<Guid>
{
    public bool IsDefault { get; set; }
    public bool IsDeleted { get; set; }
}

public class AppRoleClaim : IdentityRoleClaim<Guid>
{
}