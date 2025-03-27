using IMS.Domain.Base;
using System.Collections.Generic;

namespace IMS.Domain.Entities
{
    public class Revenue : GeneralBase
    {

        public Guid? Id { get; set; }

        public string? Listid { get; set; }

        public string? Description { get; set; }


        public Guid? ParentAccountId { get; set; }


        public Revenue? ParentAccount { get; set; }


        public ICollection<Revenue>? Children { get; set; } = new List<Revenue>();
    }
}