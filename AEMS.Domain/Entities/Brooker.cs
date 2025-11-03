using IMS.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Domain.Entities
{
    public class Brooker : GeneralBase
    {
   
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrookerNumber { get; set; }
        public string? Name { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
    }
}