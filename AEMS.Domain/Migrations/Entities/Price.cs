using IMS.Domain.Base;
using System;

namespace IMS.Domain.Entities
{
    public class Price : GeneralBase
    {

        public Guid? ProductId { get; set; } 
        public Product? Product { get; set; } 

        public decimal? BasePrice { get; set; } 

        public decimal? SalePrice { get; set; } 

        public DiscountType? DiscountType { get; set; } 

        public decimal? DiscountValue { get; set; } 

        public DateTime? DiscountStartDate { get; set; } 

        public DateTime? DiscountEndDate { get; set; } 

        public string? Currency { get; set; } 

        public decimal? RetailPrice { get; set; } 

        public decimal? SpecialOfferPrice { get; set; } 

        public decimal? TaxRate { get; set; } 

        public DateTime? EffectiveDate { get; set; } 

        public DateTime? LastUpdatedDate { get; set; } 

        public bool? IsActive { get; set; } 

        public string? Remarks { get; set; }
    }

    public enum DiscountType
    {
        Percentage = 1,
        Fixed = 2
    }
}
