using IMS.Domain.Base;
using System;

namespace IMS.Domain.Entities
{
    public class Stock : GeneralBase
    {
        public Guid? ProductId { get; set; } 
        public Product? Product { get; set; }    
        public int? QuantityAvailable { get; set; }
        public int? QuantityReserved { get; set; }
        public int? QuantitySold { get; set; }
        public int? ReorderLevel { get; set; }
        public int? MaxStockLevel { get; set; }
        public Guid? UnitId { get; set; }
        public Unit? Unit { get; set; }
        public string? BatchNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? StockEntryDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string? WarehouseLocation { get; set; }
        public Guid? SupplierId { get; set; } 
        public decimal? PurchasePrice { get; set; }
        public decimal? CurrentStockValue { get; set; }
        public bool? IsActive { get; set; }
        public string? StockMovementHistory { get; set; } 
    }

   
}