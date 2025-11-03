using IMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZMS.Domain.Entities
{
    public class Charges : GeneralBase
    {
        public Guid? Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChargeNo { get; set; }
        public string? ChargeDate { get; set; }
        public string? OrderNo { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdationDate { get; set; }
        public string? Status { get; set; }
        public List<ChargeLine>? Lines { get; set; }
        public List<ChargesPayments>? Payments { get; set; }
    }

    public class ChargeLine
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

    public class ChargesPayments
    {
        public Guid? Id { get; set; }
        public float? PaidAmount { get; set; }
        public string? BankCash { get; set; }
        public string? ChqNo { get; set; }
        public string? ChqDate { get; set; }
        public string? PayNo { get; set; }
    }
}