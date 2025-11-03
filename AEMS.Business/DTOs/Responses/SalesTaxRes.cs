using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace IMS.Business.DTOs.Requests
{

    public class SalesTaxRes
    {
        public Guid? Id { get; set; }
        public int? SalesTaxNumber { get; set; }
        public string? TaxName { get; set; }
        public string? TaxType { get; set; }
        public string? Percentage { get; set; }
        public string? ReceivableAccountId { get; set; }
        public string? ReceivableDescription { get; set; }
        public string? PayableAccountId { get; set; }
        public string? PayableDescription { get; set; }
    }


}