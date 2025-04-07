using IMS.Domain.Base;

namespace IMS.Business.DTOs.Responses
{
    public class AccountIdRes
    {
        public Guid? Id { get; set; }
        public string Listid { get; set; }
        public string Description { get; set; }
      //  public AccountType AccountType { get; set; }
        public Guid? ParentAccountId { get; set; }
        public List<AccountIdRes> Children { get; set; } = new List<AccountIdRes>();
    }
}
