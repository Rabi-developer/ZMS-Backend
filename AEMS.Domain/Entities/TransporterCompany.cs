using IMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMS.Domain.Entities
{
    public class TransporterCompany : GeneralBase
    {
   
        public string? Listid { get; set; }
        public string? Descriptions { get; set; }
        public string? Segment { get; set; }
    }
}
