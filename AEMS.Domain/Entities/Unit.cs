using IMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Entities
{
    public class Unit : GeneralBase
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string BaseUnit { get; set; }
        public int Operator { get; set; }
        public string OperatorValue { get; set; }

    }
}
