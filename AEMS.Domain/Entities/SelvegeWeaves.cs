using IMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Entities
{
    public class SelvegeWeaves : GeneralBase
    {
        public Guid? Id { get; set; }
        public string? Listid { get; set; }
        public string? Descriptions { get; set; }
        public string? SubDescription { get; set; }
    }
}
