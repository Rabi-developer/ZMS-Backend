using IMS.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace IMS.Domain.Entities;

public class Employee : GeneralBase
{
       [Required]
    public string Name { get; set; }

    
    public string? EmployeeFirstName { get; set; }

    public string? EmployeeMiddleName { get; set; }

    [Required]
    public string EmployeeLastName { get; set; }

    [Required]
    public string Gender { get; set; }

    [Required]
    [Phone]
    public string MobileNumber { get; set; }

    [Required]
    public DateTime HireDate { get; set; }

    public string? Status { get; set; }

    [Required]
    public string CNICNumber { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string IndustryType { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public string Country { get; set; }

    [Required]
    public string EmploymentType { get; set; }

    [Required]
    public string Position { get; set; }

    public string? Description { get; set; }

    // Additional fields if needed
    public string? MaritalStatus { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
}