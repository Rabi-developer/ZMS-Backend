using IMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMS.Domain.Entities;

public class Supplier : GeneralBase
{
    [Required]
    public string SupplierName { get; set; }

    [Required]
    public string SupplierId { get; set; }

    [Required]
    public string ContactPerson { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public string SupplierType { get; set; }

    public List<stringlist>? AccountNumbers { get; set; }

    public string? Notes { get; set; } 

    // Additional fields if needed
    public string? Website { get; set; }
    public string? TaxId { get; set; }
    public string? PaymentTerms { get; set; }
    public DateTime? ContractStartDate { get; set; }
    public DateTime? ContractEndDate { get; set; }
}

public class stringlist
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}