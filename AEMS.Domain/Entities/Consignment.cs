using IMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZMS.Domain.Entities
{
    public class Consignment : GeneralBase
    {
       
        public string? ConsignmentMode { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReceiptNo { get; set; }
        public string? OrderNo { get; set; }
        public string? BiltyNo { get; set; }
        public string? Date { get; set; }
        public string? ConsignmentNo { get; set; }
        public string? Consignor { get; set; }
        public string? ConsignmentDate { get; set; }
        public string? CreditAllowed { get; set; }
        public string? Consignee { get; set; }
        public string? ReceiverName { get; set; }
        public string? ReceiverContactNo { get; set; }
        public string? ShippingLine { get; set; }
        public string? ContainerNo { get; set; }
        public string? Port { get; set; }
        public string? Destination { get; set; }
        public string? FreightFrom { get; set; }
        public float? TotalQty { get; set; }
        public string? Freight { get; set; }
        public string? SbrTax { get; set; }
        public float? SprAmount { get; set; }
        public string? DeliveryCharges { get; set; }
        public string? InsuranceCharges { get; set; }
        public string? TollTax { get; set; }
        public string? OtherCharges { get; set; }
        public float? TotalAmount { get; set; }
        public float? ReceivedAmount { get; set; }
        public float? IncomeTaxDed { get; set; }
        public float? IncomeTaxAmount { get; set; }
        public string? DeliveryDate { get; set; }
        public string? Remarks { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdationDate { get; set; }
        public string? Status { get; set; }
        public List<ConsignmentItem>? Items { get; set; }
    }

    public class ConsignmentItem
    {
        public Guid? Id { get; set; }
        public string? Desc { get; set; }
        public float? Qty { get; set; }
        public float? Rate { get; set; }
        public string? QtyUnit { get; set; }
        public float? Weight { get; set; }
        public string? WeightUnit { get; set; }
    }
}