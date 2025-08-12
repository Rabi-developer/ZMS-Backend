using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace IMS.Business.DTOs.Requests
{
    public class VendorRes 
    {
        public Guid? Id { get; set; }
        public string? VendorNumber { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? Stn { get; set; }
        public string? Ntn { get; set; }
        public string? PayableCode { get; set; }
    }


}