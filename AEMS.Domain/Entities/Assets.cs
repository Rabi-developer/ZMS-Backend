using IMS.Domain.Base;
using System.Collections.Generic;

namespace IMS.Domain.Entities
{
    public class Assets : GeneralBase
    {


        public string? Listid { get; set; }

        public string? Description { get; set; }


        public Guid? ParentAccountId { get; set; }


        public Assets? ParentAccount { get; set; }


        public ICollection<Assets>? Children { get; set; } = new List<Assets>();
    }
}