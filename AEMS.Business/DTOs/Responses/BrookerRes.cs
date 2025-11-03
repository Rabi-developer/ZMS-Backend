using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace IMS.Business.DTOs.Requests
{
    public class  BrookerRes
    {
        public Guid? Id { get; set; }
        public int? BrookerNumber { get; set; }
        public string? Name { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
    }


}