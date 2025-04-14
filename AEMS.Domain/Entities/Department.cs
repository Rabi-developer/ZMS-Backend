using IMS.Domain.Base;

namespace IMS.Domain.Entities;

public class Department : GeneralBase
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string? HeadOfDepartment { get; set; }
    public Guid? AddressId { get; set; }
    public Address? Address { get; set; }
    public Guid? BranchId { get; set; }
    public Branch? Branch { get; set; }

}
