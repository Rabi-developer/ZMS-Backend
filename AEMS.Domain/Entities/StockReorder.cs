using IMS.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Domain.Entities
{
    public class StockReorder : GeneralBase
    {
       
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }

        public int? ReorderQuantity { get; set; } 

        public DateTime? ReorderDate { get; set; } 

        public int? CurrentStockLevel { get; set; } 

        public Guid? SupplierId { get; set; } 

        public ReorderStatus? Status { get; set; } 

        public DateTime? OrderDate { get; set; } 

        public DateTime? ExpectedDeliveryDate { get; set; } 
      
   //  public virtual Supplier Supplier { get; set; }
    }

  
}
