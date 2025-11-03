using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace IMS.Domain.Entities
{
    public class AblRevenue : GeneralBase
    {
     
        public string? Listid { get; set; }
        public string? Description { get; set; }
        public Guid? ParentAccountId { get; set; }
        public AblRevenue? ParentAccount { get; set; }
        public ICollection<AblRevenue>? Children { get; set; } = new List<AblRevenue>();
        public string? DueDate { get; set; }
        public string FixedAmount { get; set; }
        public string Paid { get; set; }
    }
}