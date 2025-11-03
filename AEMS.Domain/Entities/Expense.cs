using IMS.Domain.Base;
using System.Collections.Generic;

namespace IMS.Domain.Entities
{
    public class Expense : GeneralBase
    {


        public string? Listid { get; set; }
        public string? Description { get; set; }


        public Guid? ParentAccountId { get; set; }


        public Expense? ParentAccount { get; set; }


        public ICollection<Expense>? Children { get; set; } = new List<Expense>();
    }
}