using IMS.Domain.Base;

namespace IMS.Business.DTOs.Requests
{
    public class AccountIdReq
    {
        public string Description { get; set; }
        public Guid? ParentAccountId { get; set; }
    //    public AccountType AccountType { get; set; }
    }
}
