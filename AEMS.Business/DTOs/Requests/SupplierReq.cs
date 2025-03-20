using IMS.Domain.Entities;

namespace IMS.Business.DTOs.Requests
{
    public class SupplierReq
    {
        public Guid? Id { get; set; } 
        public string SupplierName { get; set; }
        public string SupplierId { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string SupplierType { get; set; }
        public List<stringlist>? AccountNumbers { get; set; }
        public string? Notes { get; set; } 

        public string? Website { get; set; }
        public string? TaxId { get; set; }
        public string? PaymentTerms { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
    }
}