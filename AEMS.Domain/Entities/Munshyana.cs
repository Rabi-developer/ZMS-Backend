using IMS.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Domain.Entities
{
    public class Munshyana : GeneralBase
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? MunshyanaNumber { get; set; }
        public string? ChargesDesc { get; set; }
        public string? ChargesType { get; set; }
        public string? AccountId { get; set; }
        public string? Description { get; set; }
    }
}