using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace ZMS.Domain.Entities
{
    public class ChargesReq
    {
        public Guid? Id { get; set; }
        public string? ChargeNo { get; set; }
        public string? ChargeDate { get; set; }
        public string? OrderNo { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdationDate { get; set; }
        public string? Status { get; set; }
        public List<ChargeLineReq>? Lines { get; set; }
        public List<ChargesPaymentsReq>? Payments { get; set; }
    }

    public class ChargeLineReq
    {
        public Guid? Id { get; set; }
        public string? Charge { get; set; }
        public string? BiltyNo { get; set; }
        public string? Date { get; set; }
        public string? Vehicle { get; set; }
        public string? PaidTo { get; set; }
        public string? Contact { get; set; }
        public string? Remarks { get; set; }
        public float? Amount { get; set; }
    }

    public class ChargesPaymentsReq
    {
        public Guid? Id { get; set; }
        public float? PaidAmount { get; set; }
        public string? BankCash { get; set; }
        public string? ChqNo { get; set; }
        public string? ChqDate { get; set; }
        public string? PayNo { get; set; }
    }
}