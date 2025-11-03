using IMS.Domain.Base;

namespace IMS.Domain.Entities;

public class AccountId : GeneralBase
{
   
    public string Listid { get; set; }
    public string Description { get; set; }
    public Guid? ParentAccountId { get; set; }
    public AccountId ParentAccount { get; set; }
    public ICollection<AccountId> Children { get; set; } = new List<AccountId>();
    public AccountType AccountType { get; set; }
}