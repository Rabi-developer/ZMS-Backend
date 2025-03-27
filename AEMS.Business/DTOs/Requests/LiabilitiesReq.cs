using IMS.Domain.Entities;

namespace IMS.Business.DTOs.Requests
{
    public class LiabilitiesReq
    {
        public Guid? Id { get; set; }

        public string? Listid { get; set; }

        public string? Description { get; set; }


        public Guid? ParentAccountId { get; set; }



        public ICollection<Liabilities>? Children { get; set; } = new List<Liabilities>();
    }
}