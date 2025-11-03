using IMS.Business.DTOs.Requests;
using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace ZMS.Business.DTOs.Requests
{
    public class EntryVoucherRes
    {
        public Guid? Id { get; set; }
        public int? VoucherNo { get; set; }
        public string? VoucherDate { get; set; }
        public string? ReferenceNo { get; set; }
        public string? ChequeNo { get; set; }
        public string? DepositSlipNo { get; set; }
        public string? PaymentMode { get; set; }
        public string? BankName { get; set; }
        public string? ChequeDate { get; set; }
        public string? PaidTo { get; set; }
        public string? Narration { get; set; }
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdationDate { get; set; }
        public string? Status { get; set; }
        public List<VoucherDetailRes>? VoucherDetails { get; set; }
    }

    public class VoucherDetailRes
    {
        public Guid? Id { get; set; }
        public string? Account1 { get; set; }
        public float? Debit1 { get; set; }
        public float? Credit1 { get; set; }
        public float? CurrentBalance1 { get; set; }
        public float? ProjectedBalance1 { get; set; }
        public string? Narration { get; set; }
        public string? Account2 { get; set; }
        public float? Debit2 { get; set; }
        public float? Credit2 { get; set; }
        public float? CurrentBalance2 { get; set; }
        public float? ProjectedBalance2 { get; set; }
    }
}

public class EntryVoucherStatus

{
    public Guid? Id { get; set; }
    public string? Status { get; set; }
}
