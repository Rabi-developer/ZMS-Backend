using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace ZMS.Domain.Entities
{
    public class ReceiptRes : GeneralBase
    {
        public Guid? Id { get; set; }
        public int? ReceiptNo { get; set; }
        public string? ReceiptDate { get; set; }
        public string? PaymentMode { get; set; }
        public string? Files { get; set; }
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
        public List<ReceiptItemRes>? Items { get; set; }
    }

    public class ReceiptItemRes
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
    public class BiltyBalanceRes
    {
        public string? BiltyNo { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? ReceivedAmount { get; set; }
        public decimal? Balance { get; set; }
    }
}

public class ReceiptStatus
{
    public Guid? Id { get; set; }
    public string? Status { get; set; }
}