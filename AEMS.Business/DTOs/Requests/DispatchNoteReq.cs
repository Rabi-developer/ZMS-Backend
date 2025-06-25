using System;
using System.Collections.Generic;
using ZMS.Domain.Entities;

namespace IMS.Business.DTOs.Requests
{
    public class DispatchNoteReq
    {
        public Guid? Id { get; set; }
        public string? Listid { get; set; }
        public string? Date { get; set; }
        public string? Bilty { get; set; }
        public string? Seller { get; set; }
        public string? Buyer { get; set; }
        public string? VehicleType { get; set; }
        public string? ContractNumber { get; set; }
        public string? Remarks { get; set; }
        public string? DriverName { get; set; }
        public string? DriverNumber { get; set; }
        public string? Transporter { get; set; }
        public string Destination { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdationDate { get; set; }
        public string? Status { get; set; }
        public List<RelatedContractReq>? RelatedContracts { get; set; }
    }

    public class RelatedContractReq
    {
        public Guid? Id { get; set; }
        public string? ContractNumber { get; set; }
        public string? Seller { get; set; }
        public string? Buyer { get; set; }
        public string? WidthOrColor { get; set; }
        public string? BuyerRefer { get; set; }
        public string? FabricDetails { get; set; }
        public string? Rate { get; set; }
        public string? Date { get; set; }
        public string? ContractQuantity { get; set; }
        public string? Base { get; set; }
        public string? Quantity { get; set; }
        public string? TotalAmount { get; set; }
        public string? DispatchQuantity { get; set; }
        public string? TotalDispatchQuantity { get; set; }
        public string? BalanceQuantity { get; set; }
        public string? ContractType { get; set; }
        public string? RowId { get; set; }
    }

    public class HistoryDispatchNote
    {

        public string Seller { set; get; }
        public string Buyer { set; get; }
    }
    }

public class DispatchNoteStatus
{
    public Guid? Id { get; set; }
    public string? Status { get; set; }
}

