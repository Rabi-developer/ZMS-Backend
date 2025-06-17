using IMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZMS.Domain.Entities
{
    public class Contract : GeneralBase
    {
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
        public string FabricType { get; set; }
        public string Description { get; set; }
        public string? DescriptionSubOptions { get; set; }
        public string Stuff { get; set; }
        public string? StuffSubOptions { get; set; }
        public string? BlendRatio { get; set; }
        public string? BlendType { get; set; }
        public string? WarpCount { get; set; }
        public string? WeftCount { get; set; }
        public string? WarpYarnType { get; set; }
        public string? WarpYarnTypeSubOptions { get; set; }
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
        public string? Selvege { get; set; }
        public string? SelvegeSubOptions { get; set; }
        public string? SelvegeWeaves { get; set; }
        public string? SelvegeWeaveSubOptions { get; set; }
        public string? SelvegeWidth { get; set; }
        public string? Quantity { get; set; }
        public string? UnitOfMeasure { get; set; }
        public string? Tolerance { get; set; }
        public string? Rate { get; set; }
        public string? Packing { get; set; }
        public string? PieceLength { get; set; }
        public string? InductionThread { get; set; }
        public string? InductionThreadSubOptions { get; set; }
        public string? FabricValue { get; set; }
        public string? Gsm { get; set; }
        public string? Gst { get; set; }
        public string? GstValue { get; set; }
        public string? TotalAmount { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdationDate { get; set; }
        public string? ApprovedBy { get; set; }
        public string? ApprovedDate { get; set; }
        public string? EndUse { get; set; }
        public string? SelvegeThickness { get; set; }
        public string? SelvegeThicknessSubOptions { get; set; }
        public string? EndUseSubOptions { get; set; }
        public string? Notes { get; set; }
        public string? DispatchLater { get; set; }
        public string? Status { get; set; }
        public string? FinishWidth { get; set; }
        public List<DeliveryBreakup>? BuyerDeliveryBreakups { get; set; }
        public List<DeliveryBreakup>? SellerDeliveryBreakups { get; set; }
        public List<ConversionContractRow>? ConversionContractRow { get; set; }
        public List<DietContractRow>? DietContractRow { get; set; }
        public List<MultiWidthContractRow>? MultiWidthContractRow { get; set; }
    }

    public class DeliveryBreakup
    {
        //
        public Guid Id { get; set; } // Changed from Guid? to Guid
        public string? Qty { get; set; }
        public string? DeliveryDate { get; set; }
    }

    public class ConversionContractRow
    {
        //
        public Guid Id { get; set; } // Added primary key
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
        public CommisionInfo? CommisionInfo { get; set; }
        public List<DeliveryBreakup>? BuyerDeliveryBreakups { get; set; }
        public List<DeliveryBreakup>? SellerDeliveryBreakups { get; set; }
    }

    public class CommisionInfo
    {
        //
        public Guid Id { get; set; }
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
        //
        public Guid Id { get; set; }
        public Guid ContractId { get; set; } // Added foreign key
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
        public CommisionInfo? CommisionInfo { get; set; }
        public List<DeliveryBreakup>? BuyerDeliveryBreakups { get; set; }
        public List<DeliveryBreakup>? SellerDeliveryBreakups { get; set; }
    }


    public class MultiWidthContractRow
    {
        //
        public Guid Id { get; set; }
        public Guid ContractId { get; set; } // Added foreign key
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
        public CommisionInfo? CommisionInfo { get; set; }
        public List<DeliveryBreakup>? BuyerDeliveryBreakups { get; set; }
        public List<DeliveryBreakup>? SellerDeliveryBreakups { get; set; }
    }

}