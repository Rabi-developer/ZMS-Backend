using IMS.Domain.Base;

namespace IMS.Domain.Entities
{
    public class Description : GeneralBase
    {
        public Guid? Id { get; set; }
        public string? Listid { get; set; }
        public string? Descriptions { get; set; } 
        public string? SubDescription { get; set; }
    }
}