/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMS.Business.DTOs.Responses
{
    public class CommisionInvoiceRes
    {

        public string? CommissionInvoiceNumber { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DueDate { get; set; }
        public string? CommissionFrom { get; set; }
        public string? Seller { get; set; }
        public string? Buyer { get; set; }
        public string? Remarks { get; set; }
        public bool ExcludeSRB { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdationDate { get; set; }

        public string? Status { get; set; }

        public List<CommissionInvoiceDetailRes>? RelatedInvoices { get; set; }
    }
    public class CommissionInvoiceDetailRes
    {
        public Guid Id { get; set; }
        public Guid? CommissionInvoiceId { get; set; }
        //public Payment? Invoice { get; set; }
        public string? InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string? Buyer { get; set; }
        public string? FabricDetails { get; set; }
        public string? InvoiceQty { get; set; }
        public string? Rate { get; set; }
        public string? InvoiceValue { get; set; }
        public string? CommissionPercent { get; set; }
        public string? Amount { get; set; }
        public string? SrTax { get; set; }
        public string? SrTaxAmount { get; set; }
        public string? TotalAmount { get; set; }
    }
}

public class HistoryDataCommisionInvoiceRes
{
    public string? InvoiceNumber { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public string? Buyer { get; set; }
    public string? FabricDetails { get; set; }
    public string? InvoiceQty { get; set; }
    public string? Rate { get; set; }
    public string? InvoiceValue { get; set; }
    public string? CommissionPercent { get; set; }
}*/