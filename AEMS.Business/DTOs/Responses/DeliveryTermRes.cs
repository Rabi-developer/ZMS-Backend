using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMS.Business.DTOs.Responses
{
    public class DeliveryTermRes
    {
        public Guid? Id { get; set; }
        public string? Listid { get; set; }
        public string? Descriptions { get; set; }
        public string? Segment { get; set; }
    }
}
