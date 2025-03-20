using IMS.Domain.Base;
using IMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace IMS.Business.DTOs.Requests;


   public class StockReorderRes
    {
    public Guid? Id { get; set; }   
    public Guid? ProductId { get; set; }
    public Product? Product { get; set; }

    public int? ReorderQuantity { get; set; }

    public DateTime? ReorderDate { get; set; }

    public int? CurrentStockLevel { get; set; }

    public Guid? SupplierId { get; set; }

    public ReorderStatus? Status { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? ExpectedDeliveryDate { get; set; }
}

