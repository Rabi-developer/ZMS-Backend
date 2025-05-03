using IMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Entities
{
    public class Buyer : GeneralBase
    {
      
        public string? BuyerName { get; set; }
        public string? BuyerType { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? DeliveryDate { get; set; }
        public string? EmailAddress { get; set; }
        public string? FaxNumber { get; set; }
        public string? MTN { get; set; }
        public string? MobileNumber { get; set; }
        public string? OrderDate { get; set; }
        public string? PayableCode { get; set; }
        public string? Payableid { get; set; }
        public string? PhoneNumber { get; set; }
        public string? STN { get; set; }
        public string? AccountNo { get; set; }

        public Guid? Seller {  get; set; }
    }

}
