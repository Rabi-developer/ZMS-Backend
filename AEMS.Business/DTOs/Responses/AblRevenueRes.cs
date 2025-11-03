using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace IMS.Business.DTOs.Responses
{
    public class AblRevenueRes : GeneralBase
    {
        public Guid? Id { get; set; }
        public string? Listid { get; set; }
        public string? Description { get; set; }
        public Guid? ParentAccountId { get; set; }
        public AblRevenueRes? ParentAccount { get; set; }
        public ICollection<AblRevenueRes>? Children { get; set; } = new List<AblRevenueRes>();
        public string? DueDate { get; set; }
        public string FixedAmount { get; set; }
        public string Paid { get; set; }
    }
}