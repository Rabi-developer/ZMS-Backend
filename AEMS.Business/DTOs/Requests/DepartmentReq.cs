namespace IMS.Business.DTOs.Requests;

public class DepartmentReq
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string? HeadOfDepartment { get; set; }
    public Guid? AddressId { get; set; }
    public Guid? BranchId { get; set; }
}

