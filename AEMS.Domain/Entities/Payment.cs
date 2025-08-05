using IMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMS.Domain.Entities
{
    public class Payment : GeneralBase
    {
        public string? PaymentNumber { get; set; }
        public string? PaymentDate { get; set; }
        public string? PaymentType { get; set; } // Advance, Payment, Income Tax
        public string? Mode { get; set; }
        public string? BankName { get; set; }
        public string? ChequeNo { get; set; }
        public string? ChequeDate { get; set; }
        public string? Seller { get; set; }
        public string? Buyer { get; set; }
        public string? PaidAmount { get; set; }
        public string? IncomeTaxAmount { get; set; }
        public string? IncomeTaxRate { get; set; }
        public string? Cpr { get; set; }
        public string? AdvanceReceived { get; set; }
        public string? Remarks { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdationDate { get; set; }
        public string? Status { get; set; }
        public List<RelatedInvoice>? RelatedInvoices { get; set; }
    }
    public class RelatedInvoice
    {
        public Guid? Id { get; set; }
        public Guid? PaymentId { get; set; }
        public Payment? Payment { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? InvoiceDate { get; set; }
        public string? DueDate { get; set; }
        public string? Seller { get; set; }
        public string? Buyer { get; set; }
        public string? TotalAmount { get; set; }
        public string? ReceivedAmount { get; set; }
        public string? Balance { get; set; }
        public string? InvoiceAdjusted { get; set; }
    }
}
