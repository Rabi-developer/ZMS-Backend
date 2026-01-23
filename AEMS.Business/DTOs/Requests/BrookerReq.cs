using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace IMS.Business.DTOs.Requests
{
    public class BrookerReq
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Mobile { get; set; }
        public string? CNIC { get; set; }
        public string AccountNumber { get; set; }
        public string? Address { get; set; }
    }


}