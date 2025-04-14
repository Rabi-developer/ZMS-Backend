using IMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMS.Domain.Entities;

public class Customer : GeneralBase
{
    [Required]
    public string CustomerName { get; set; }

    [Required]
    public string CustomerId { get; set; }

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
    public string CustomerType { get; set; }
    public List<stringlist>? AccountNumbers { get; set; }

    public string? Notes { get; set; } 

    public string? Website { get; set; }
    public string? TaxId { get; set; }
    public string? PaymentTerms { get; set; }
    public DateTime? ContractStartDate { get; set; }
    public DateTime? ContractEndDate { get; set; }
}