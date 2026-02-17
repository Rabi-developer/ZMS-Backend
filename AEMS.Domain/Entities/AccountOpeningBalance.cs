using IMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ZMS.Domain.Entities
{
    public class AccountOpeningBalance : GeneralBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountOpeningNo { get; set; }
        public string AccountOpeningDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdationDate { get; set; }
        public string? Status { get; set; }
        public List<AccountOpeningBalanceEntry>? AccountOpeningBalanceEntrys { get; set; }
    }
    public class AccountOpeningBalanceEntry
    {
        public Guid? Id { get; set; }
        public Guid? AccountOpeningBalanceId { get; set; }
        public string? Account { get; set; }
        public float? Debit { get; set; }
        public float? Credit { get; set; }
        public string? Narration { get; set; }

    }
}