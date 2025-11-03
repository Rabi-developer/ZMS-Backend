using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace IMS.Domain.Entities
{
    public class InspectionNote : GeneralBase
    {
  
        public string IrnNumber { get; set; }
        public string IrnDate { get; set; }
        public string Seller { get; set; }
        public string? InvoiceNumber { get; set; }
        public string Buyer { get; set; }
        public string DispatchNoteId { get; set; }
        public string? Remarks { get; set; }
        public List<InspectionContract> RelatedContracts { get; set; }
        public string? Status { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdationDate { get; set; }

    }

    public class InspectionContract : GeneralBase
    {
        public Guid? Id { get; set; }
        public string? ContractNumber { get; set; }
        public string? Quantity { get; set; }
        public string? DispatchQty { get; set; }
        public string? BGrade { get; set; }
        public string? Sl { get; set; }
        public string? Shrinkage { get; set; }  
        public string? ReturnFabric {  get; set; }
        public string? AGrade { get; set; }
        public string? InspectedBy { get; set; }

    }
}