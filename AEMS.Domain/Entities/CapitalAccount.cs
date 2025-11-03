using IMS.Domain.Base;
using System.Collections.Generic;

namespace IMS.Domain.Entities
{
    public class CapitalAccount : GeneralBase
    {

   
        public string? Listid { get; set; }

        public string? Description { get; set; }


        public Guid? ParentAccountId { get; set; }


        public CapitalAccount? ParentAccount { get; set; }


        public ICollection<CapitalAccount>? Children { get; set; } = new List<CapitalAccount>();
    }
}