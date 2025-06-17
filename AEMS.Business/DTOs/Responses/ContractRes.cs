using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace ZMS.Domain.Entities
{
    public class ContracRes : GeneralBase
    {
//
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
        public string? SelvegeThickness { get; set; }
        public string? SelvegeThicknessSubOptions { get; set; }
        public string? EndUseSubOptions { get; set; }
        public string? Notes { get; set; }
        public string? DispatchLater { get; set; }
        public string? SellerCommission { get; set; }
        public string? BuyerCommission { get; set; }
        public string? Status { get; set; }
        public string? FinishWidth { get; set; }
        public List<DeliveryBreakuRes>? BuyerDeliveryBreakuRess { get; set; }
        public List<DeliveryBreakuRes>? SellerDeliveryBreakuRess { get; set; }
    //    public List<SampleDetailRes>? SampleDetailsRes { get; set; }
        public List<ConversionContractRowRes>? ConversionContractRowRes { get; set; }
        public List<DietContractRowRes>? DietContractRowRes { get; set; }
        public List<MultiWidthContractRowRes>? MultiWidthContractRowRes { get; set; }
    }

    public class DeliveryBreakuRes
    {
//
        public string? Qty { get; set; }
        public string? DeliveryDate { get; set; }
    }

    /*public class SampleDetailRes
    {
        // public Guid Id { get; set; }
        public string? SampleQty { get; set; }
        public string? SampleReceivedDate { get; set; }
        public string? SampleDeliveredDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdateDate { get; set; }
        public List<AdditionalInfoRes>? AdditionalInfoRes { get; set; }
    }*/
    /*public class AdditionalInfoRes
    {
//
        public string? EndUse { get; set; }
        public string? Count { get; set; }
        public string? Weight { get; set; }
        public string? YarnBags { get; set; }
        public string? Labs { get; set; }
    }*/
    public class ConversionContractRowRes
    {
//
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
        public List<CommisionInfoRes>? CommisionInfoRe { get; set; }
        public List<DeliveryBreakuRes>? DeliveryBreakuRes { get; set; }
    }

    public class CommisionInfoRes
    {
//
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

    public class DietContractRowRes
    {
//
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
        public List<DietContractRowResListRes>? DietContractRowResListRes { get; set; }
        public List<DeliveryBreakuRes>? DeliveryBreakuRes { get; set; }
    }
    public class DietContractRowResListRes
    {

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
    public class MultiWidthContractRowRes
    {
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
        public List<MultiWidthContractRowResInfo>? MultiWidthContractRowResInfo { get; set; }
        public List<DeliveryBreakuRes>? DeliveryBreakuRes { get; set; }

    }

    public class MultiWidthContractRowResInfo
    {
//
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
}
public class ContractStatus
{
    public Guid Id { get; set; }
    public string? Status { get; set; }
}