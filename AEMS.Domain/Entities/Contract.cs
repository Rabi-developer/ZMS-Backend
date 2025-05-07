using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace ZMS.Domain.Entities
{
    public class Contract : GeneralBase
    {
        public Guid? Id { get; set; }
        public string? ContractNumber { get; set; }
        public string? Date { get; set; }
        public string? ContractType { get; set; }
        public string? CompanyId { get; set; }
        public string? BranchId { get; set; }
        public string? ContractOwner { get; set; }
        public string? Seller { get; set; }
        public string? Buyer { get; set; }
        public string? ReferenceNumber { get; set; }
        public string? DeliveryDate { get; set; }
        public string? Refer { get; set; }
        public string? Referdate { get; set; }
        public string? FabricType { get; set; }
        public string? DescriptionId { get; set; }
        public string? Stuff { get; set; }
        public string? BlendRatio { get; set; }
        public string? BlendType { get; set; }
        public string? WarpCount { get; set; }
        public string? WarpYarnType { get; set; }
        public string? WeftCount { get; set; }
        public string? WeftYarnType { get; set; }
        public string? NoOfEnds { get; set; }
        public string? NoOfPicks { get; set; }
        public string? Weaves { get; set; }
        public string? PickInsertion { get; set; }
        public string? Width { get; set; }
        public string? Final { get; set; }
        public string? Selvedge { get; set; }
        public string? SelvedgeWeave { get; set; }
        public string? SelvedgeWidth { get; set; }
        public string? Quantity { get; set; }
        public string? UnitOfMeasure { get; set; }
        public string? Tolerance { get; set; }
        public string? Rate { get; set; }
        public string? Packing { get; set; }
        public string? PieceLength { get; set; }
        public string? FabricValue { get; set; }
        public string? Gst { get; set; }
        public string? GstValue { get; set; }
        public string? TotalAmount { get; set; }
        public string? PaymentTermsSeller { get; set; }
        public string? PaymentTermsBuyer { get; set; }
        public string? DeliveryTerms { get; set; }
        public string? CommissionFrom { get; set; }
        public string? CommissionType { get; set; }
        public string? CommissionPercentage { get; set; }
        public string? CommissionValue { get; set; }
        public string? DispatchAddress { get; set; }
        public string? SellerRemark { get; set; }
        public string? BuyerRemark { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdationDate { get; set; }
        public string? ApprovedBy { get; set; }
        public string? ApprovedDate { get; set; }
        public string? EndUse { get; set; }
        public List<DeliveryBreakup>? BuyerDeliveryBreakups { get; set; }
        public List<DeliveryBreakup>? SellerDeliveryBreakups { get; set; }
        public List<SampleDetail>? SampleDetails { get; set; }
    }

    public class DeliveryBreakup
    {
        public Guid? Id { get; set; }
        public string? Qty { get; set; }
        public string? DeliveryDate { get; set; }
    }

    public class SampleDetail
    {
        public Guid? Id { get; set; }
        public string? SampleQty { get; set; }
        public string? SampleReceivedDate { get; set; }
        public string? SampleDeliveredDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdateDate { get; set; }
        public List<AdditionalInfo>? AdditionalInfo { get; set; }
    }

    public class AdditionalInfo
    {
        public Guid? Id { get; set; }
        public string? EndUse { get; set; }
        public string? Count { get; set; }
        public string? Weight { get; set; }
        public string? YarnBags { get; set; }
        public string? Labs { get; set; }
    }
}