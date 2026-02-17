using IMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZMS.Domain.Entities
{
    public class OpeningBalanceReq
    {
        public Guid? Id { get; set; }


        public string? OpeningDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdationDate { get; set; }
        public string? Status { get; set; }
        public List<OpeningBalanceEntryReq>? OpeningBalanceEntrys { get; set; }
    }

    public class OpeningBalanceEntryReq
    {
        public Guid? Id { get; set; }
        public string? BiltyNo { get; set; }
        public string? BiltyDate { get; set; }
        public string? VehicleNo { get; set; }
        public string? City { get; set; }
        public string? Customer { get; set; }
        public string? Broker { get; set; }
        public string? ChargeType { get; set; }
        public float? Debit { get; set; }
        public float? Credit { get; set; }


    }
}
public class OpeningBalanceStatusRequest
{
    public int OpeningNo { get; set; }
    public string Status { get; set; } = string.Empty;
}