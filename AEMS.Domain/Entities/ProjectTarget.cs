using IMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMS.Domain.Entities;

public class ProjectTarget : GeneralBase
{
    [Required]
    public string TargetPeriod { get; set; }

    [Required]
    public DateTime TargetDate { get; set; }

    [Required]
    public DateTime TargetEndDate { get; set; }

    [Required]
    public decimal TargetValue { get; set; }

    [Required]
    public string Purpose { get; set; }

    [Required]
    public string ProjectStatus { get; set; }

    [Required]
    public string ProjectManager { get; set; }

    [Required]
    public string FinancialHealth { get; set; }

    [Required]
    public string BuyerName { get; set; }

    [Required]
    public string SellerName { get; set; }

    public string? StepsToComplete { get; set; }

    public string? Attachments { get; set; }

    [Required]
    public string EmployeeId { get; set; }

    [Required]
    public string EmployeeType { get; set; }

    public DateTime? DueDate { get; set; }

    [Required]
    public string ApprovedBy { get; set; }

    [Required]
    public DateTime ApprovalDate { get; set; }

    // Navigation properties
    public Employee Employee { get; set; }
}