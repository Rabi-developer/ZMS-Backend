using IMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Business.DTOs.Requests
{
    public class PriceReq
    {
        public Guid? Id { get; set; }
        public Guid? ProductId { get; set; }
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
}
