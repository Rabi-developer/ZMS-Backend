using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace ZMS.Domain.Entities
{
    public class BiltyPaymentInvoiceReq
    {
        public Guid? Id { get; set; }
        public string? InvoiceNo { get; set; }
        public string PaymentDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdationDate { get; set; }
        public string? Status { get; set; }
        public List<BiltyPaymentInvoiceLineReq>? Lines { get; set; }
    }

    public class BiltyPaymentInvoiceLineReq
    {
        public Guid? Id { get; set; }
        public bool IsAdditionalLine { get; set; }
        public string? VehicleNo { get; set; }
        public string? OrderNo { get; set; }
        public float? Amount { get; set; }
        public string? NameCharges { get; set; }
        public float? AmountCharges { get; set; }
        public string? Munshayana { get; set; }
        public string? Broker { get; set; }
        public string? DueDate { get; set; }
        public string? Remarks { get; set; }
    }
}