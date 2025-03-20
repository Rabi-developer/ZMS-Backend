using IMS.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Domain.Entities;

public class StockMovement : GeneralBase
{

    public Guid? ProductId { get; set; }
    public Product? Product { get; set; }

    public MovementType? MovementType { get; set; } 
    public int? Quantity { get; set; } 
    public DateTime? DateOfMovement { get; set; } 

    public string? SourceLocation { get; set; } 

    public string? DestinationLocation { get; set; } 
    public string? ReasonForMovement { get; set; } 

    public string? Remarks { get; set; } 

   
}

