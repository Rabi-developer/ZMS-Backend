using IMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZMS.Domain.Entities
{
    public class BiltyPaymentInvoice : GeneralBase
    {
   
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceNo { get; set; }
        public string PaymentDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdationDate { get; set; }
        public string? Status { get; set; }
        public List<BiltyPaymentInvoiceLine>? Lines { get; set; }
    }

    public class BiltyPaymentInvoiceLine
    {
        public Guid? Id { get; set; }
        public bool IsAdditionalLine { get; set; }
        public string? VehicleNo { get; set; }
        public string? OrderNo { get; set; }
        public float? Amount { get; set; }
        public string? NameCharges { get; set; }
        public float? AmountCharges { get; set; }
        public float? Munshayana { get; set; }
        public string? Broker { get; set; }
        public string? DueDate { get; set; }
        public string? Remarks { get; set; }
    }
}