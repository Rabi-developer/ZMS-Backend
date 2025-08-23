using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace ZMS.Domain.Entities
{
    public class PaymentABLRes : GeneralBase
    {
        public Guid? Id { get; set; }
        public string? PaymentNo { get; set; }
        public string? PaymentDate { get; set; }
        public string? PaymentMode { get; set; }
        public string? BankName { get; set; }
        public string? ChequeNo { get; set; }
        public string? ChequeDate { get; set; }
        public string? PaidTo { get; set; }
        public float? PaidAmount { get; set; }
        public string? Remarks { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdationDate { get; set; }
        public string? Status { get; set; }
        public List<PaymentABLItemRes>? Items { get; set; }
    }

    public class PaymentABLItemRes
    {
        public Guid? Id { get; set; }
        public string? VehicleNo { get; set; }
        public string? OrderNo { get; set; }
        public string? Charges { get; set; }
        public string? OrderDate { get; set; }
        public string? DueDate { get; set; }
        public float? ExpenseAmount { get; set; }
        public float? Balance { get; set; }
        public float? PaidAmount { get; set; }
    }
}

public class PaymentAblStatus
{
    public Guid? Id { get; set; }
    public string? Status { get; set; }
}