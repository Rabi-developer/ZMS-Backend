using IMS.Domain.Base;
using System;

namespace IMS.Domain.Entities
{
    public class SalesTax : GeneralBase
    {
        public Guid? Id { get; set; }
        public string? SalesTaxNumber { get; set; }
        public string? TaxName { get; set; }
        public string? TaxType { get; set; }
        public string? Percentage { get; set; }
        public string? ReceivableAccountId { get; set; }
        public string? ReceivableDescription { get; set; }
        public string? PayableAccountId { get; set; }
        public string? PayableDescription { get; set; }
      
    }
}