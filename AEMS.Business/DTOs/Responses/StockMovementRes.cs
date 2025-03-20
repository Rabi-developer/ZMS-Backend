using IMS.Domain.Base;
using IMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace IMS.Business.DTOs.Requests;

public class StockMovementRes
{
    public Guid? Id { get; set; }
    public Guid? ProductId { get; set; }
    public MovementType? MovementType { get; set; }
    public int? Quantity { get; set; }
    public DateTime? DateOfMovement { get; set; }
    public string? SourceLocation { get; set; }
    public string? DestinationLocation { get; set; }
    public string? ReasonForMovement { get; set; }
    public string? Remarks { get; set; }
}
