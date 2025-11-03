using System;
using System.Collections.Generic;

namespace ZMS.Domain.Entities
{
    public class ReceiptReq
    {
        public Guid? Id { get; set; }
        public string? ReceiptDate { get; set; }
        public string? PaymentMode { get; set; }
        public string? BankName { get; set; }
        public string? ChequeNo { get; set; }
        public string? ChequeDate { get; set; }
        public string? Party { get; set; }
        public decimal? ReceiptAmount { get; set; }
        public string? Remarks { get; set; }
        public string? SalesTaxOption { get; set; }
        public string? SalesTaxRate { get; set; }
        public string? WhtOnSbr { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdationDate { get; set; }
        public string? Status { get; set; }
        public List<ReceiptItemReq>? Items { get; set; }
    }

    public class ReceiptItemReq
    {
        public Guid? Id { get; set; }
        public string? BiltyNo { get; set; }
        public string? VehicleNo { get; set; }
        public string? BiltyDate { get; set; }
        public decimal? BiltyAmount { get; set; }
        public decimal? SrbAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? Balance { get; set; }
        public decimal? ReceiptAmount { get; set; }
    }
}