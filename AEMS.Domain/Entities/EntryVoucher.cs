using IMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZMS.Domain.Entities
{
    public class EntryVoucher : GeneralBase
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoucherNo { get; set; }
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
        public List<VoucherDetail>? VoucherDetails { get; set; }
    }

    public class VoucherDetail
    {
        public Guid? Id { get; set; }
        public string? Account1 { get; set; }
        public string? Debit1 { get; set; }
        public string? Credit1 { get; set; }
        public string? CurrentBalance1 { get; set; }
        public string? ProjectedBalance1 { get; set; }
        public string? Narration { get; set; }
        public string? Account2 { get; set; }
        public string? Debit2 { get; set; }
        public string? Credit2 { get; set; }
        public string? CurrentBalance2 { get; set; }
        public string? ProjectedBalance2 { get; set; }
    }
}