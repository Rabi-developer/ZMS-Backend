using System;
using System.Collections.Generic;

namespace IMS.Business.DTOs.Requests
{
    public class InspectionNoteRes
    {
        public Guid? Id { get; set; }
        public string? IrnNumber { get; set; }
        public string? IrnDate { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? Seller { get; set; }
        public string? Buyer { get; set; }
        public string? DispatchNoteId { get; set; }
        public string? Remarks { get; set; }
        public string? CreatedBy { get; set; }
        public string? Status { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdationDate { get; set; }
        public List<RelatedInspectionContractRes>? RelatedContracts { get; set; }
    }

    public class RelatedInspectionContractRes
    {
        public Guid? Id { get; set; }
        public string? ContractNumber { get; set; }
        public string? Quantity { get; set; }
        public string? DispatchQty { get; set; }
        public string? BGrade { get; set; }
        public string? Sl { get; set; }
        public string? Shrinkage { get; set; }
        public string? ReturnFabric { get; set; }
        public string? AGrade { get; set; }
        public string? InspectedBy { get; set; }
    }
}
public class InspectionNoteStatus
{
    public Guid? Id { get; set; }
    public string? Status { get; set; }
}