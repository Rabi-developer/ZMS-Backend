using System.ComponentModel.DataAnnotations;
using IMS.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Domain.Entities
{
    public class Vendor : GeneralBase
    {
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorNumber { get; set; }
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