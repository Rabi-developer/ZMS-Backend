using IMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Entities
{
   public class Product : GeneralBase
    {
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public string? SKU { get; set; }
        public string Description { get; set; }
        public Attachment? FeaturedImage { get; set; }
        public List<Attachment>? Images { get; set; }
        public string? ProductType { get; set; }
        public string? ProductCost { get; set; }
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        public int? Stock { get; set; }
        public string? StockAlert { get; set; }


    }
}
