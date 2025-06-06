﻿using System;
using System.Collections.Generic;

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
        public string? Vehicle { get; set; }
        public string? ContractNumber { get; set; }
        public string? Remarks { get; set; }
        public string? DriverName { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdationDate { get; set; }
        public List<RelatedContractReq>? RelatedContracts { get; set; }
    }

    public class RelatedContractReq
    {
        public Guid? Id { get; set; }
        public string? ContractNumber { get; set; }
        public string? Seller { get; set; }
        public string? Buyer { get; set; }
        public string? Date { get; set; }
        public string? Quantity { get; set; }
        public string? TotalAmount { get; set; }

    public string? Base { get; set; }
        public string? DispatchQty {  get; set; }
    }
}