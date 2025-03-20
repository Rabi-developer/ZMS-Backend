using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Business.DTOs.Requests
{
    public class UnitReq
    {

        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string BaseUnit { get; set; }
        public int Operator { get; set; }
        public string OperatorValue { get; set; }
    }

}
