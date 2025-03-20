using IMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Entities
{
    public class Brand : GeneralBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
       
    }
}
