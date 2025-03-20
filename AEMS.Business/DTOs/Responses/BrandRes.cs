using IMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Business.DTOs.Requests
{
    public class BrandRes
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
       
    }
}
