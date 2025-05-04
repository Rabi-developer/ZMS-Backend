using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Business.DTOs.Requests
{
    public class GeneralSaleTextTypeReq
    {

        public Guid? Id { get; set; }
        public string? GstType { get; set; }
        public string? Percentage { get; set; }

    }

}
