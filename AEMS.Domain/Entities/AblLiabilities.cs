using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace IMS.Domain.Entities
{
    public class AblLiabilities : GeneralBase
    {
       
        public string? Listid { get; set; }
        public string? Description { get; set; }
        public Guid? ParentAccountId { get; set; }
        public AblLiabilities? ParentAccount { get; set; }
        public ICollection<AblLiabilities>? Children { get; set; } = new List<AblLiabilities>();
        public string? DueDate { get; set; }
        public string FixedAmount { get; set; }
        public string Paid { get; set; }
    }
}