using IMS.Domain.Base;
using System;

namespace IMS.Domain.Entities
{
    public class Munshyana : GeneralBase
    {
        public Guid? Id { get; set; }
        public string? MunshyanaNumber { get; set; }
        public string? ChargesDesc { get; set; }
        public string? ChargesType { get; set; }
        public string? AccountId { get; set; }
        public string? Description { get; set; }
    }
}