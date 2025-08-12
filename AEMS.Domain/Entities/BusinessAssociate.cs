using IMS.Domain.Base;
using System;

namespace IMS.Domain.Entities
{
    public class BusinessAssociate : GeneralBase
    {
        public Guid? Id { get; set; }
        public string? BusinessAssociateNumber { get; set; }
        public string? Name { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
    }
}