using System;
using System.Collections.Generic;

namespace IMS.Application.Contracts
{
    public class ContractReq
    {
        public string ContractNumber { get; set; } 
        public DateTime Date { get; set; }
        public string ContractType { get; set; } 
        public Guid CompanyId { get; set; }
        public Guid BranchId { get; set; }
        public string ContractOwner { get; set; } 
        public Guid SellerId { get; set; }
        public Guid BuyerId { get; set; }
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
        public string? WarpYarnTypeSubOptions { get; set; } // Pipe-separated
        public string? WeftCount { get; set; }
        public string WeftYarnType { get; set; }
        public string? WeftYarnTypeSubOptions { get; set; } // Pipe-separated
        public string? NoOfEnds { get; set; }
        public string? NoOfPicks { get; set; }
        public string? Weaves { get; set; }
        public string? WeavesSubOptions { get; set; } // Pipe-separated
        public string? PickInsertion { get; set; }
        public string? PickInsertionSubOptions { get; set; } // Pipe-separated
        public string? Width { get; set; }
        public string? Final { get; set; }
        public string? FinalSubOptions { get; set; } // Pipe-separated
        public string? Selvege { get; set; }
        public string? SelvegeSubOptions { get; set; } // Pipe-separated
        public string? SelvegeWeave { get; set; }
        public string? SelvegeWeaveSubOptions { get; set; } // Pipe-separated
        public string? SelvegeWidth { get; set; }
        public string? InductionThread { get; set; }
        public string? InductionThreadSubOptions { get; set; } // Pipe-separated
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
        public string? PaymentTermsSeller { get; set; } // Dropdown
        public string? PaymentTermsBuyer { get; set; } // Dropdown
        public string? DeliveryTerms { get; set; } // Dropdown
        public string? CommissionFrom { get; set; } // Dropdown
        public string? CommissionType { get; set; }
        public string? CommissionPercentage { get; set; }
        public string? CommissionValue { get; set; }
        public string? DispatchAddress { get; set; } // Input
        public string? SellerRemark { get; set; } // Input
        public string? BuyerRemark { get; set; } // Input
        public string? EndUse { get; set; } // Dropdown
        public string? EndUseSubOptions { get; set; } // Pipe-separated
        public string? Notes { get; set; }
        public string? SelvegeThickness { get; set; }
        public string? DispatchLater { get; set; } // Dropdown
        public string? SellerCommission { get; set; } // Input, Pipe-separated
        public string? BuyerCommission { get; set; } // Input, Pipe-separated
        public string? Status { get; set; }

        // Nested collections
        public List<BuyerDeliveryBreakupReq> BuyerDeliveryBreakups { get; set; } = new List<BuyerDeliveryBreakupReq>();
        public List<SellerDeliveryBreakupReq> SellerDeliveryBreakups { get; set; } = new List<SellerDeliveryBreakupReq>();
        public List<SampleDetailReq> SampleDetails { get; set; } = new List<SampleDetailReq>();
        public List<ConversionContractRowReq> ConversionContractRows { get; set; } = new List<ConversionContractRowReq>();
        public List<DietContractRowReq> DietContractRows { get; set; } = new List<DietContractRowReq>();
        public List<MultiWidthContractRowReq> MultiWidthContractRows { get; set; } = new List<MultiWidthContractRowReq>();
    }

    public class BuyerDeliveryBreakupReq
    {
        public string Qty { get; set; } = string.Empty;
        public DateTime DeliveryDate { get; set; }
    }

    public class SellerDeliveryBreakupReq
    {
        public string Qty { get; set; } = string.Empty;
        public DateTime DeliveryDate { get; set; }
    }

    public class SampleDetailReq
    {
        public string? SampleQty { get; set; }
        public DateTime? SampleReceivedDate { get; set; }
        public DateTime? SampleDeliveredDate { get; set; }

        public List<AdditionalInfoReq> AdditionalInfos { get; set; } = new List<AdditionalInfoReq>();
    }

    public class AdditionalInfoReq
    {
        public Guid id { get; set; }
        public string? EndUse { get; set; }
        public string? Count { get; set; }
        public string? Weight { get; set; }
        public string? YarnBags { get; set; }
        public string? Labs { get; set; }
    }

    public class ConversionContractRowReq
    {
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
        public List<CommisionInfoReq> CommisionInfo { get; set; } = new List<CommisionInfoReq>();
    }

    public class CommisionInfoReq
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

    public class DietContractRowReq
    {
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
        public List<CommisionInfoReq> CommisionInfo { get; set; } = new List<CommisionInfoReq>();
    }

    public class MultiWidthContractRowReq
    {
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
        public List<CommisionInfoReq> CommisionInfo { get; set; } = new List<CommisionInfoReq>();
    }
}