using IMS.Domain.Base;

namespace IMS.Domain.Entities
{
    public class Description : GeneralBase
    {
     
        public string? Listid { get; set; }
        public string? Descriptions { get; set; } 
        public string? SubDescription { get; set; }
    }
}