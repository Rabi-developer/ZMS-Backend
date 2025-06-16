using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace IMS.Domain.Entities
{
    public class Contract : GeneralBase
    {
        public string ContractNumber { get; set; }
        public DateTime Date { get; set; }
        public string ContractType { get; set; }
        public Guid CompanyId { get; set; }
        public Guid BranchId { get; set; }
        public string ContractOwner { get; set; }
        public Guid SellerId { get; set; }
        public Seller? Seller { get; set; }
        public Guid BuyerId { get; set; }
        public Buyer? Buyer { get; set; }
        public string? ReferenceNumber { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string? Refer { get; set; }
        public DateTime? Referdate { get; set; }
        public string FabricType { get; set; } 
        public string Description { get; set; } 
        public string? DescriptionSubOptions { get; set; } 
        public string Stuff { get; set; }
        public string? StuffSubOptions { get; set; } 
        public string? BlendRatio { get; set; }
        public string? BlendType { get; set; }
        public string? WarpCount { get; set; }
        public string? WarpYarnType { get; set; }
        public string? WarpYarnTypeSubOptions { get; set; } 
        public string? WeftCount { get; set; }
        public string WeftYarnType { get; set; } 
        public string? WeftYarnTypeSubOptions { get; set; } 
        public string? NoOfEnds { get; set; }
        public string? NoOfPicks { get; set; }
        public string? Weaves { get; set; }
        public string? WeavesSubOptions { get; set; } 
        public string? PickInsertion { get; set; }
        public string? PickInsertionSubOptions { get; set; } 
        public string? Width { get; set; }
        public string? Final { get; set; }
        public string? FinalSubOptions { get; set; } 
        public string? Selvege { get; set; }
        public string? SelvegeSubOptions { get; set; } 
        public string? SelvegeWeave { get; set; }
        public string? SelvegeWeaveSubOptions { get; set; } 
        public string? SelvegeWidth { get; set; }
        public string? InductionThread { get; set; }
        public string? InductionThreadSubOptions { get; set; } 
        public string? GSM { get; set; }
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
        public DateTime? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdationDate { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? EndUse { get; set; }
        public string? EndUseSubOptions { get; set; }
        public string? Notes { get; set; }
        public string? SelvegeThickness { get; set; }
        public string? DispatchLater { get; set; }
        public string? SellerCommission { get; set; } 
        public string? BuyerCommission { get; set; } 

        public string? Status {  get; set; }

        // Navigation properties
        public virtual ICollection<BuyerDeliveryBreakup> BuyerDeliveryBreakups { get; set; } = new List<BuyerDeliveryBreakup>();
        public virtual ICollection<SellerDeliveryBreakup> SellerDeliveryBreakups { get; set; } = new List<SellerDeliveryBreakup>();
        public virtual ICollection<SampleDetail> SampleDetails { get; set; } = new List<SampleDetail>();
        public virtual ICollection<ConversionContractRow> ConversionContractRows { get; set; } = new List<ConversionContractRow>();
        public virtual ICollection<DietContractRow> DietContractRows { get; set; } = new List<DietContractRow>();
        public virtual ICollection<MultiWidthContractRow> MultiWidthContractRows { get; set; } = new List<MultiWidthContractRow>();
    }

    public class BuyerDeliveryBreakup 
    {
        public Guid id { get; set; }
        public Guid ContractId { get; set; }
        public Contract? Contract { get; set; }
        public string Qty { get; set; }
        public DateTime DeliveryDate { get; set; }
    }

    public class SellerDeliveryBreakup
    {
        public Guid ContractId { get; set; }
        public Contract? Contract { get; set; }
        public string Qty { get; set; }
        public DateTime DeliveryDate { get; set; }
    }

    public class SampleDetail 
    {
        public Guid ContractId { get; set; }
        public Contract? Contract { get; set; }
        public string? SampleQty { get; set; }
        public DateTime? SampleReceivedDate { get; set; }
        public DateTime? SampleDeliveredDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<AdditionalInfo> AdditionalInfos { get; set; } = new List<AdditionalInfo>();
    }

    public class AdditionalInfo 
    {
        public Guid id { get; set; }
        public Guid SampleDetailId { get; set; }
        public SampleDetail? SampleDetail { get; set; }
        public string? EndUse { get; set; }
        public string? Count { get; set; }
        public string? Weight { get; set; }
        public string? YarnBags { get; set; }
        public string? Labs { get; set; }
    }

    public class ConversionContractRow
    {
        public Guid id { get; set; }
        public Guid ContractId { get; set; }
        public Contract? Contract { get; set; }
        public string? Width { get; set; }
        public string Quantity { get; set; }
        public string? PickRate { get; set; }
        public string? FabRate { get; set; }
        public string Rate { get; set; } 
        public string? Amounts { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string? Wrapwt { get; set; }
        public string? Weftwt { get; set; }
        public string? WrapBag { get; set; }
        public string? WeftBag { get; set; }
        public string? TotalAmountMultiple { get; set; }
        public string Gst { get; set; }
        public string? GstValue { get; set; }
        public string? FabricValue { get; set; }
        public string? CommissionType { get; set; }
        public string? CommissionPercentage { get; set; }
        public string? CommissionValue { get; set; }
        public string? TotalAmount { get; set; }
        public List<CommisionInfo> CommisionInfo { get; set; } = new List<CommisionInfo>();

    }

    public class CommisionInfo
    {
        public Guid id { get; set; }
        public string? PaymentTermsSeller { get; set; }
        public string? PaymentTermsBuyer { get; set; } 
        public string? DeliveryTerms { get; set; } 
        public string? CommissionFrom { get; set; } 
        public string? DispatchAddress { get; set; }
        public string? SellerRemark { get; set; } 
        public string? BuyerRemark { get; set; } 
        public string? EndUse { get; set; }
        public string? EndUseSubOptions { get; set; }
        public string? DispatchLater { get; set; } 
        public string? SellerCommission { get; set; }
        public string? BuyerCommission { get; set; } 

    }

    public class DietContractRow 
    {
        public Guid id { get; set; }
        public Guid ContractId { get; set; }
        public Contract? Contract { get; set; }
        public string? LabDispatchNo { get; set; }
        public DateTime? LabDispatchDate { get; set; }
        public string? Color { get; set; }
        public string Quantity { get; set; }
        public string? Finish { get; set; }
        public string Rate { get; set; } 
        public string? AmountTotal { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Gst { get; set; } 
        public string? GstValue { get; set; }
        public string? FabricValue { get; set; }
        public string? CommissionType { get; set; }
        public string? CommissionPercentage { get; set; }
        public string? CommissionValue { get; set; }
        public string? TotalAmount { get; set; }
        public string? Shrinkage { get; set; }
        public string? FinishWidth { get; set; }
        public string? Weight { get; set; }
        public List<CommisionInfo> CommisionInfo { get; set; } = new List<CommisionInfo>();

    }

    public class MultiWidthContractRow 
    {
        public Guid id { get; set; }
        public Guid ContractId { get; set; }
        public Contract? Contract { get; set; }
        public string? Width { get; set; }
        public string Quantity { get; set; }
        public string Rate { get; set; }
        public string? Amount { get; set; }
        public string Gst { get; set; }
        public string? GstValue { get; set; }
        public string? FabricValue { get; set; }
        public string? CommissionType { get; set; }
        public string? CommissionPercentage { get; set; }
        public string? CommissionValue { get; set; }
        public string? TotalAmount { get; set; }
        public List<CommisionInfo> CommisionInfo { get; set; } = new List<CommisionInfo>();

    }
}