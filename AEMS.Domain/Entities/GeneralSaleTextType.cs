using IMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Entities
{
    public class GeneralSaleTextType : GeneralBase
    {
        public string? GstType { get; set; }
        public string? Percentage { get; set; }

    }
}
