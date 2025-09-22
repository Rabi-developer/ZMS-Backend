using IMS.Business.DTOs.Requests;
using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace ZMS.Domain.Entities
{
    public class BookingOrderRes
    {
        public Guid? Id { get; set; }
        public string? OrderNo { get; set; }
        public string? OrderDate { get; set; }
        public string? Transporter { get; set; }
        public string? Vendor { get; set; }
        public string? VehicleNo { get; set; }
        public string? ContainerNo { get; set; }
        public string? VehicleType { get; set; }
        public string? DriverName { get; set; }
        public string? ContactNo { get; set; }
        public string? Munshayana { get; set; }
        public string? CargoWeight { get; set; }
        public string? BookedDays { get; set; }
        public string? DetentionDays { get; set; }
        public string? FromLocation { get; set; }
        public string? DepartureDate { get; set; }
        public string? Via1 { get; set; }
        public string? Via2 { get; set; }
        public string? ToLocation { get; set; }
        public string? ExpectedReachedDate { get; set; }
        public string? ReachedDate { get; set; }
        public string? VehicleMunshyana { get; set; }
        public string? Remarks { get; set; }
        public string? ContractOwner { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdationDate { get; set; }
        public string? Status { get; set; }
       


    }

    public class RelatedConsignmentRes
    {
        public Guid? Id { get; set; }
        public Guid? BookingOrderId { get; set; }
        public string? BiltyNo { get; set; }
        public string? ReceiptNo { get; set; }
        public string? Consignor { get; set; }
        public string? Consignee { get; set; }
        public string? Item { get; set; }
        public int? Qty { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? RecvAmount { get; set; }
        public string? DelDate { get; set; }
        public string? Status { get; set; }
    }
}

public class BookingOrderStatus

{
    public Guid? Id { get; set; }
    public string? Status { get; set; }
}