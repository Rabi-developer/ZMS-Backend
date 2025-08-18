using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace ZMS.Domain.Entities
{
    public class ConsignmentRes
    {
        public Guid? Id { get; set; }
        public string? ConsignmentMode { get; set; }
        public string? ReceiptNo { get; set; }
        public string? OrderNo { get; set; }
        public string? BiltyNo { get; set; }
        public string? Date { get; set; }
        public string? ConsignmentNo { get; set; }
        public string? Consignor { get; set; }
        public string? ConsignmentDate { get; set; }
        public string? Consignee { get; set; }
        public string? ReceiverName { get; set; }
        public string? ReceiverContactNo { get; set; }
        public string? ShippingLine { get; set; }
        public string? ContainerNo { get; set; }
        public string? Port { get; set; }
        public string? Destination { get; set; }
        public string? FreightFrom { get; set; }
        public decimal? TotalQty { get; set; }
        public decimal? Freight { get; set; }
        public string? SbrTax { get; set; }
        public decimal? SprAmount { get; set; }
        public decimal? DeliveryCharges { get; set; }
        public decimal? InsuranceCharges { get; set; }
        public decimal? TollTax { get; set; }
        public decimal? OtherCharges { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? ReceivedAmount { get; set; }
        public decimal? IncomeTaxDed { get; set; }
        public decimal? IncomeTaxAmount { get; set; }
        public string? DeliveryDate { get; set; }
        public string? Remarks { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdationDate { get; set; }
        public string? Status { get; set; }
        public List<ConsignmentItemRes>? Items { get; set; }
    }

    public class ConsignmentItemRes
    {
        public Guid? Id { get; set; }
        public string? Desc { get; set; }
        public decimal? Qty { get; set; }
        public string? QtyUnit { get; set; }
        public decimal? Weight { get; set; }
        public string? WeightUnit { get; set; }
    }
}

public class ConsignmentStatus
{
    public Guid? Id { get; set; }
    public string? Status { get; set; }
}