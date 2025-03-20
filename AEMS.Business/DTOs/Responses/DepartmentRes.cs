﻿namespace IMS.Business.DTOs.Responses;

public class DepartmentRes
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string? HeadOfDepartment { get; set; }
    public Guid? AddressId { get; set; }
    public AddressRes? Address { get; set; }
    public Guid? BranchId { get; set; }
    public string BranchName { get; set; }
}