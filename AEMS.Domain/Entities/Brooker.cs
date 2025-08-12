using IMS.Domain.Base;
using System;

namespace IMS.Domain.Entities
{
    public class Brooker : GeneralBase
    {
        public Guid? Id { get; set; }
        public string? BrookerNumber { get; set; }
        public string? Name { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
    }
}