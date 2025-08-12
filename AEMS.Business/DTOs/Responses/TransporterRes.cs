using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace IMS.Business.DTOs.Requests
{
    public class TransporterRes : GeneralBase
    {
        public Guid? Id { get; set; }
        public string? TransporterNumber { get; set; }
        public string? Name { get; set; }
        public string? Currency { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? BankName { get; set; }
        public string? Tel { get; set; }
        public string? Ntn { get; set; }
        public string? Mobile { get; set; }
        public string? Stn { get; set; }
        public string? Fax { get; set; }
        public string? BuyerCode { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
    }


}