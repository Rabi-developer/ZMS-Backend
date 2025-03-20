using IMS.Domain.Base;

namespace IMS.Domain.Entities;

public class Address : GeneralBase
{
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string Zip { get; set; }
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; }
}
