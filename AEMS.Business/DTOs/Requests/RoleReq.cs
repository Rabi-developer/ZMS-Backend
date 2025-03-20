using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Business.DTOs.Requests
{
    public class RoleReq
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<RoleClaimReq> Claims { get; set; }

    }

    public class RoleClaimReq

    {
        public int Id { get; set; }
        public Guid RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
