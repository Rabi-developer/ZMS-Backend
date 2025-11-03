using IMS.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Domain.Entities
{
    public class BusinessAssociate : GeneralBase
    {
  
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BusinessAssociateNumber { get; set; }
        public string? Name { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
    }
}