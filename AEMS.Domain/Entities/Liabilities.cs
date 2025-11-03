using IMS.Domain.Base;
using System.Collections.Generic;

namespace IMS.Domain.Entities
{
    public class Liabilities : GeneralBase
    {

     

        public string? Listid { get; set; }

        public string? Description { get; set; }


        public Guid? ParentAccountId { get; set; }


        public Liabilities? ParentAccount { get; set; }


        public ICollection<Liabilities>? Children { get; set; } = new List<Liabilities>();
    }
}