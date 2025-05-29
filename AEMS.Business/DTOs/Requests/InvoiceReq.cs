﻿using IMS.Business.DTOs.Requests;
using IMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace ZMS.Domain.Entities
{
    public class InvoiceReq : GeneralBase
    {
        public Guid? Id { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? InvoiceDate { get; set; }
        public string? DueDate { get; set; }
        public string? InvoiceReceivedDate { get; set; }
        public string? InvoiceDeliveredByDate { get; set; }
        public string? Seller { get; set; }
        public string? Buyer { get; set; }
        public string? Remarks { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreationDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdationDate { get; set; }
        public List<RelatedInvoiceContractReq>? RelatedContracts { get; set; }
        
    }

    public class RelatedInvoiceContractReq
    {
        public Guid? Id { get; set; }
        public string? ContractNumber { get; set; }
        public string? FabricDetails { get; set; }

        public string? Seller { get; set; }
        public string? Buyer { get; set; }
        public string? Date { get; set; }
        public string? Quantity { get; set; }
        public string? TotalAmount { get; set; }
        public string? DispatchQty { get; set; }
        public string? InvoiceQty { get; set; }
        public string? InvoiceRate { get; set; }

        public string? Gst { get; set; }
        public string? GstPercentage { get; set; }
        public string? GstValue { get; set; }
        public string? InvoiceValueWithGst { get; set; }
        public string? WhtPercentage { get; set; }
        public string? WhtValue { get; set; }
        public string? TotalInvoiceValue { get; set; }
    }
}