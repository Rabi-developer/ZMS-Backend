using IMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMS.Business.DTOs.Requests
{
    public class MunshyanaReq
    {
        public Guid? Id { get; set; }
        public string? MunshyanaNumber { get; set; }
        public string? ChargesDesc { get; set; }
        public string? ChargesType { get; set; }
        public string? AccountId { get; set; }
        public string? Description { get; set; }
    }
}
