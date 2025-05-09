using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using ZMS.Domain.Entities;

namespace IMS.Business.Services;

public interface IContractService : IBaseService<ContractReq, ContractRes, Contract>
{
    Task<ContractRes> UpdateStatusAsync(ContractStatus status);
}

public class ContractService : BaseService<ContractReq, ContractRes, ContractRepository, Contract>, IContractService
{
    private readonly IContractRepository _repository;
    private readonly IHttpContextAccessor _context;

    public ContractService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<ContractRepository>();
        _context = context;
    }

    public async Task<ContractRes> UpdateStatusAsync(ContractStatus status)
    {
        if (status == null || status.Id == null || string.IsNullOrWhiteSpace(status.Status))
        {
            throw new ArgumentException("Contract ID and Status are required.");
        }

        var validStatuses = new[] { "Pending", "Approved", "Canceled", "Closed Dispatch", "Closed Payment", "Complete Closed" };
        if (!validStatuses.Contains(status.Status))
        {
            throw new ArgumentException($"Status must be one of: {string.Join(", ", validStatuses)}");
        }

        var contract = await _repository.GetByIdAsync(status.Id.Value);
        if (contract == null)
        {
            throw new KeyNotFoundException($"Contract with ID {status.Id} not found.");
        }

        contract.Status = status.Status;
        contract.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
        contract.UpdationDate = DateTime.UtcNow.ToString("o");

        await _repository.UpdateAsync(contract);
        await UnitOfWork.SaveAsync();

        return MapToResponse(contract);
    }

    private ContractRes MapToResponse(Contract contract)
    {
        if (contract == null)
        {
            throw new ArgumentNullException(nameof(contract));
        }

        return new ContractRes
        {
            Id = contract.Id,
            ContractNumber = contract.ContractNumber,
            ContractType = contract.ContractType,
            CompanyId = contract.CompanyId,
            BranchId = contract.BranchId,
            ContractOwner = contract.ContractOwner,
            Seller = contract.Seller,
            Buyer = contract.Buyer,
            ReferenceNumber = contract.ReferenceNumber,
            DeliveryDate = contract.DeliveryDate,
            Refer = contract.Refer,
            Referdate = contract.Referdate,
            FabricType = contract.FabricType,
            DescriptionId = contract.DescriptionId,
            Stuff = contract.Stuff,
            BlendRatio = contract.BlendRatio,
            BlendType = contract.BlendType,
            WarpCount = contract.WarpCount,
            WarpYarnType = contract.WarpYarnType,
            WeftCount = contract.WeftCount,
            WeftYarnType = contract.WeftYarnType,
            NoOfEnds = contract.NoOfEnds,
            NoOfPicks = contract.NoOfPicks,
            Weaves = contract.Weaves,
            PickInsertion = contract.PickInsertion,
            Width = contract.Width,
            Final = contract.Final,
            Selvedge = contract.Selvedge,
            SelvedgeWeave = contract.SelvedgeWeave,
            SelvedgeWidth = contract.SelvedgeWidth,
            Quantity = contract.Quantity,
            UnitOfMeasure = contract.UnitOfMeasure,
            Tolerance = contract.Tolerance,
            Rate = contract.Rate,
            Packing = contract.Packing,
            PieceLength = contract.PieceLength,
            FabricValue = contract.FabricValue,
            Gst = contract.Gst,
            GstValue = contract.GstValue,
            TotalAmount = contract.TotalAmount,
            PaymentTermsSeller = contract.PaymentTermsSeller,
            PaymentTermsBuyer = contract.PaymentTermsBuyer,
            DeliveryTerms = contract.DeliveryTerms,
            CommissionFrom = contract.CommissionFrom,
            CommissionType = contract.CommissionType,
            CommissionPercentage = contract.CommissionPercentage,
            CommissionValue = contract.CommissionValue,
            DispatchAddress = contract.DispatchAddress,
            SellerRemark = contract.SellerRemark,
            BuyerRemark = contract.BuyerRemark,
            CreatedBy = contract.CreatedBy,
            CreationDate = contract.CreationDate,
            UpdatedBy = contract.UpdatedBy,
            UpdationDate = contract.UpdationDate,
            ApprovedBy = contract.ApprovedBy,
            ApprovedDate = contract.ApprovedDate,
            EndUse = contract.EndUse,
            BuyerDeliveryBreakups = contract.BuyerDeliveryBreakups?.Select(b => new DeliveryBreakupRes
            {
                Id = b.Id,
                Qty = b.Qty,
                DeliveryDate = b.DeliveryDate
            }).ToList(),
            SellerDeliveryBreakups = contract.SellerDeliveryBreakups?.Select(s => new DeliveryBreakupRes
            {
                Id = s.Id,
                Qty = s.Qty,
                DeliveryDate = s.DeliveryDate
            }).ToList(),
            SampleDetails = contract.SampleDetails?.Select(s => new SampleDetailRes
            {
                Id = s.Id,
                SampleQty = s.SampleQty,
                SampleReceivedDate = s.SampleReceivedDate,
                SampleDeliveredDate = s.SampleDeliveredDate,
                CreatedBy = s.CreatedBy,
                CreationDate = s.CreationDate,
                UpdateDate = s.UpdateDate,
                AdditionalInfo = s.AdditionalInfo?.Select(a => new AdditionalInfoRes
                {
                    Id = a.Id,
                    EndUse = a.EndUse,
                    Count = a.Count,
                    Weight = a.Weight,
                    YarnBags = a.YarnBags,
                    Labs = a.Labs
                }).ToList()
            }).ToList()
        };
    }
}