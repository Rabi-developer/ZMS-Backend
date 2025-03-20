namespace IMS.Business.DTOs.Responses;

public class AddressRes
{
    public Guid Id { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string Zip { get; set; }
    public Guid BranchId { get; set; }
}
