﻿using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Context;
using IMS.Domain.Entities;
using IMS.Domain.Migrations;
using IMS.Domain.Utilities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;
using ZMS.Domain.Entities;

namespace IMS.Business.Services;

public interface IContractService : IBaseService<ContractReq, ContractRes, Contract>
{
    public Task<ContractStatus> UpdateStatusAsync(Guid id, string status);
}

public class ContractService : BaseService<ContractReq, ContractRes, ContractRepository, Contract>, IContractService
{
    private readonly IContractRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public ContractService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<ContractRepository>();
        _context = context;
        _DbContext = dbContextn;
    }
    public async override Task<Response<IList<ContractRes>>> GetAll(Pagination? paginate)
    {
        try
        {
            var pagination = paginate ?? new Pagination();

            // Fetch paged data
            var (pag, data) = await Repository.GetAll(
                pagination,
                query => query
                    .Include(p => p.BuyerDeliveryBreakups)
                    .Include(p => p.SellerDeliveryBreakups)
                    .Include(p => p.ConversionContractRow)
                        .ThenInclude(p => p.CommisionInfo)
                   .Include(p => p.DietContractRow)
                        .ThenInclude(p => p.CommisionInfo)
                    .Include(p => p.MultiWidthContractRow)
                        .ThenInclude(p => p.CommisionInfo)
            );            // Fetch all lookup tables
            var fabricTypes = await _DbContext.FabricTypes.ToListAsync();
            var stuffTypes = await _DbContext.Stuffs.ToListAsync();
            var blendRatios = await _DbContext.BlendRatio.ToListAsync();
            var final = await _DbContext.Finals.ToListAsync();
            var description = await _DbContext.Descriptions.ToListAsync();
            var warpYarnTypes = await _DbContext.WrapYarnTypes.ToListAsync();
            var weftYarnTypes = await _DbContext.WeftYarnTypes.ToListAsync();
            var weavesList = await _DbContext.Weaves.ToListAsync();
            var pickInsertions = await _DbContext.PickInsertion.ToListAsync();
            var packings = await _DbContext.Packings.ToListAsync();
            var pieceLengths = await _DbContext.Peicelengths.ToListAsync();
            var endUses = await _DbContext.EndUses.ToListAsync();
            var gst = await _DbContext.GeneralSaleTexts.ToListAsync();
            var selvege = await _DbContext.Selveges.ToListAsync();
            var selvegeWeaves = await _DbContext.SelvegeWeaves.ToListAsync();
            var selvegeWidth = await _DbContext.SelvegeWidths.ToListAsync();
            var peicelengths = await _DbContext.Peicelengths.ToListAsync();
            var sellers = await _DbContext.Sellers.ToListAsync();
            var buyers = await _DbContext.Buyers.ToListAsync();
            var inductionThread = await _DbContext.InductionThreads.ToListAsync();
            var selvegeThickness = await _DbContext.SelvegeThicknesses.ToListAsync();
            var gsm = await _DbContext.Gsms.ToListAsync();
            var deliveryterm = await _DbContext.DeliveryTerms.ToListAsync();
            var paymentTerms = await _DbContext.PaymentTerms.ToListAsync();








            // Map to DTO
            var result = data.Adapt<List<ContractRes>>();

            // Replace listid values with Descriptions
            // Replace listid values with Descriptions
            foreach (var item in result)
            {
                if (!string.IsNullOrWhiteSpace(item.FabricType))
                    item.FabricType = fabricTypes.FirstOrDefault(f => f.Listid == item.FabricType)?.Descriptions;

             /*   if (!string.IsNullOrWhiteSpace(item.PaymentTermsBuyer))
                    item.PaymentTermsBuyer = paymentTerms.FirstOrDefault(f => f.Listid == item.PaymentTermsBuyer)?.Descriptions;
              
                if (!string.IsNullOrWhiteSpace(item.PaymentTermsSeller))
                    item.PaymentTermsSeller = paymentTerms.FirstOrDefault(f => f.Listid == item.PaymentTermsSeller)?.Descriptions;
*/

                /*if (!string.IsNullOrWhiteSpace(item.DeliveryTerms))
                    item.DeliveryTerms = deliveryterm.FirstOrDefault(f => f.Listid == item.DeliveryTerms)?.Descriptions;
*/
                if (!string.IsNullOrWhiteSpace(item.Stuff))
                    item.Stuff = stuffTypes.FirstOrDefault(s => s.Listid == item.Stuff)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.Selvege))
                    item.Selvege = selvege.FirstOrDefault(s => s.Listid == item.Selvege)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.Description))
                    item.Description = description.FirstOrDefault(s => s.Listid == item.Description)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.SelvegeWeaves))
                    item.SelvegeWeaves = selvegeWeaves.FirstOrDefault(s => s.Listid == item.SelvegeWeaves)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.SelvegeWidth))
                    item.SelvegeWidth = selvegeWidth.FirstOrDefault(s => s.Listid == item.SelvegeWidth)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.BlendRatio))
                    item.BlendRatio = blendRatios.FirstOrDefault(b => b.Listid == item.BlendRatio)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.Final))
                    item.Final = final.FirstOrDefault(w => w.Listid == item.Final)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.WarpYarnType))
                    item.WarpYarnType = warpYarnTypes.FirstOrDefault(w => w.Listid == item.WarpYarnType)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.WeftYarnType))
                    item.WeftYarnType = weftYarnTypes.FirstOrDefault(w => w.Listid == item.WeftYarnType)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.Weaves))
                    item.Weaves = weavesList.FirstOrDefault(w => w.Listid == item.Weaves)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.PickInsertion))
                    item.PickInsertion = pickInsertions.FirstOrDefault(p => p.Listid == item.PickInsertion)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.SelvegeThickness))
                    item.SelvegeThickness = selvegeThickness.FirstOrDefault(p => p.Listid == item.SelvegeThickness)?.Descriptions;


                if (!string.IsNullOrWhiteSpace(item.InductionThread))
                    item.InductionThread = inductionThread.FirstOrDefault(p => p.Listid == item.InductionThread)?.Descriptions;


                if (!string.IsNullOrWhiteSpace(item.Gsm))
                    item.Gsm = gsm.FirstOrDefault(p => p.Listid == item.Gsm)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.Packing))
                    item.Packing = packings.FirstOrDefault(p => p.Listid == item.Packing)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.PieceLength))
                    item.PieceLength = pieceLengths.FirstOrDefault(p => p.Listid == item.PieceLength)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.EndUse))
                    item.EndUse = endUses.FirstOrDefault(e => e.Listid == item.EndUse)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.Gst))
                    item.Gst = gst.FirstOrDefault(g => g.Id.ToString() == item.Gst)?.GstType;

                if (!string.IsNullOrWhiteSpace(item.Seller))
                    item.Seller = sellers.FirstOrDefault(s => s.Id.ToString() == item.Seller)?.SellerName;

                if (!string.IsNullOrWhiteSpace(item.Buyer))
                    item.Buyer = buyers.FirstOrDefault(b => b.Id.ToString() == item.Buyer)?.BuyerName;

                // Fixed property names and consistency
            }

            return new Response<IList<ContractRes>>
            {
                Data = result,
                Misc = pag,
                StatusMessage = "Fetched successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<ContractRes>>
            {
                StatusMessage = e.InnerException?.Message ?? e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
    public async Task<ContractStatus> UpdateStatusAsync(Guid id, string status)
    {
        if (status == null || id == null)
        {
            throw new ArgumentException("Contract ID and Status are required.");
        }

        var validStatuses = new[] { "Pending", "Approved", "Canceled", "Closed Dispatch", "Closed Payment", "Complete Closed", "Conversion", "Dyed" , "MultiWidth" };
        if (!validStatuses.Contains(status))
        {
            throw new ArgumentException($"Status must be one of: {string.Join(", ", validStatuses)}");
        }

        var contract = await _DbContext.contracts.Where(p => p.Id == id).FirstOrDefaultAsync();

        if (contract == null)
        {
            throw new KeyNotFoundException($"Contract with ID {id} not found.");
        }

        contract.Status = status;
        contract.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
        contract.UpdationDate = DateTime.UtcNow.ToString("o");

        await UnitOfWork.SaveAsync();

        return new ContractStatus
        {
            Id = id,
            Status = status,
        };
    }


    public async override Task<Response<ContractRes>> Get(Guid id)
    {
        try
        {
            var entity = await Repository.Get(id, query => query
        .Include(p => p.BuyerDeliveryBreakups)
        .Include(p => p.SellerDeliveryBreakups)
        .Include(p => p.ConversionContractRow)
            .ThenInclude(p => p.CommisionInfo)
       .Include(p => p.DietContractRow)
            .ThenInclude(p => p.CommisionInfo)
        .Include(p => p.MultiWidthContractRow)
            .ThenInclude(p => p.CommisionInfo)
);
            if (entity == null)
            {
                return new Response<ContractRes>
                {
                    StatusMessage = $"{typeof(Contract).Name} Not found",
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            return new Response<ContractRes>
            {
                Data = entity.Adapt<ContractRes>(),
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ContractRes>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async override Task<Response<bool>> Delete(Guid id)
    {
        try
        {
            // Load the contract with all related entities, including nested relationships
            var contract = await UnitOfWork._context.contracts
                .Include(c => c.ConversionContractRow)
                    .ThenInclude(ccr => ccr.CommisionInfo)
                .Include(c => c.ConversionContractRow)
                    .ThenInclude(ccr => ccr.BuyerDeliveryBreakups)
                .Include(c => c.ConversionContractRow)
                    .ThenInclude(ccr => ccr.SellerDeliveryBreakups)
                .Include(c => c.DietContractRow)
                    .ThenInclude(dcr => dcr.CommisionInfo)
                .Include(c => c.DietContractRow)
                    .ThenInclude(dcr => dcr.BuyerDeliveryBreakups)
                .Include(c => c.DietContractRow)
                    .ThenInclude(dcr => dcr.SellerDeliveryBreakups)
                .Include(c => c.MultiWidthContractRow)
                    .ThenInclude(mwcr => mwcr.CommisionInfo)
                .Include(c => c.MultiWidthContractRow)
                    .ThenInclude(mwcr => mwcr.BuyerDeliveryBreakups)
                .Include(c => c.MultiWidthContractRow)
                    .ThenInclude(mwcr => mwcr.SellerDeliveryBreakups)
                .Include(c => c.BuyerDeliveryBreakups)
                .Include(c => c.SellerDeliveryBreakups)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contract == null)
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusMessage = "Contract not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            // Remove nested related rows in the correct order to avoid FK constraints
            if (contract.ConversionContractRow != null)
            {
                foreach (var row in contract.ConversionContractRow)
                {
                    // Remove nested BuyerDeliveryBreakups and SellerDeliveryBreakups
                    if (row.BuyerDeliveryBreakups != null)
                        UnitOfWork._context.RemoveRange(row.BuyerDeliveryBreakups);
                    if (row.SellerDeliveryBreakups != null)
                        UnitOfWork._context.RemoveRange(row.SellerDeliveryBreakups);
                    // Remove CommisionInfo
                    if (row.CommisionInfo != null)
                        UnitOfWork._context.Remove(row.CommisionInfo);
                }
                UnitOfWork._context.RemoveRange(contract.ConversionContractRow);
            }

            if (contract.DietContractRow != null)
            {
                foreach (var row in contract.DietContractRow)
                {
                    // Remove nested BuyerDeliveryBreakups and SellerDeliveryBreakups
                    if (row.BuyerDeliveryBreakups != null)
                        UnitOfWork._context.RemoveRange(row.BuyerDeliveryBreakups);
                    if (row.SellerDeliveryBreakups != null)
                        UnitOfWork._context.RemoveRange(row.SellerDeliveryBreakups);
                    // Remove CommisionInfo
                    if (row.CommisionInfo != null)
                        UnitOfWork._context.Remove(row.CommisionInfo);
                }
                UnitOfWork._context.RemoveRange(contract.DietContractRow);
            }

            if (contract.MultiWidthContractRow != null)
            {
                foreach (var row in contract.MultiWidthContractRow)
                {
                    // Remove nested BuyerDeliveryBreakups and SellerDeliveryBreakups
                    if (row.BuyerDeliveryBreakups != null)
                        UnitOfWork._context.RemoveRange(row.BuyerDeliveryBreakups);
                    if (row.SellerDeliveryBreakups != null)
                        UnitOfWork._context.RemoveRange(row.SellerDeliveryBreakups);
                    // Remove CommisionInfo
                    if (row.CommisionInfo != null)
                        UnitOfWork._context.Remove(row.CommisionInfo);
                }
                UnitOfWork._context.RemoveRange(contract.MultiWidthContractRow);
            }

            // Remove top-level BuyerDeliveryBreakups and SellerDeliveryBreakups
            if (contract.BuyerDeliveryBreakups != null)
                UnitOfWork._context.RemoveRange(contract.BuyerDeliveryBreakups);
            if (contract.SellerDeliveryBreakups != null)
                UnitOfWork._context.RemoveRange(contract.SellerDeliveryBreakups);

            // Remove the contract itself
            UnitOfWork._context.contracts.Remove(contract);

            // Save changes to the database
            await UnitOfWork._context.SaveChangesAsync();

            return new Response<bool>
            {
                Data = true,
                StatusMessage = "Deleted successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<bool>
            {
                Data = false,
                StatusMessage = e.InnerException?.Message ?? e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}