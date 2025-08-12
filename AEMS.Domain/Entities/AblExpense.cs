using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace IMS.Domain.Entities
{
    public class AblExpense : GeneralBase
    {
        public Guid? Id { get; set; }
        public string? Listid { get; set; }
        public string? Description { get; set; }
        public Guid? ParentAccountId { get; set; }
        public AblExpense? ParentAccount { get; set; }
        public ICollection<AblExpense>? Children { get; set; } = new List<AblExpense>();
        public string? DueDate { get; set; }
        public string FixedAmount { get; set; }
        public string Paid { get; set; }
    }
}