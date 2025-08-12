/*using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Context;
using IMS.Domain.Entities;
using IMS.Domain.Utilities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;
using ZMS.Business.DTOs.Responses;
using ZMS.DataAccess.Repositories;
using ZMS.Domain.Entities;
using System.Linq;
using System.Collections.Generic;

namespace IMS.Business.Services;

public interface ICommisionInvoiceService : IBaseService<CommisionInvoiceReq, CommisionInvoiceRes, CommisionInvoice>
{
    Task<Response<Guid>> Add(CommisionInvoiceReq reqModel);
    Task<CommisionInvoiceStatus> UpdateStatusAsync(Guid id, string status);
    Task<Response<IList<HistoryDataCommisionInvoiceRes>>> GetHistoryData(string? seller, string? buyer, Pagination? paginate);
}

public class CommisionInvoiceService : BaseService<CommisionInvoiceReq, CommisionInvoiceRes, CommisionInvoiceRepository, CommisionInvoice>, ICommisionInvoiceService
{
    private readonly DataAccess.Repositories.ICommisionInvoiceRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public CommisionInvoiceService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContext) : base(unitOfWork)
    {
        _repository = (DataAccess.Repositories.ICommisionInvoiceRepository?)UnitOfWork.GetRepository<CommisionInvoiceRepository>();
        _context = context;
        _DbContext = dbContext;
    }

    public async override Task<Response<IList<CommisionInvoiceRes>>> GetAll(Pagination? paginate)
    {
        try
        {
            var pagination = paginate ?? new Pagination();
            var (pag, data) = await Repository.GetAll(pagination, query => query.Include(p => p.RelatedInvoices));

            return new Response<IList<CommisionInvoiceRes>>
            {
                Data = data.Adapt<List<CommisionInvoiceRes>>(),
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<CommisionInvoiceRes>>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async virtual Task<Response<CommisionInvoiceRes>> Get(Guid id)
    {
        try
        {
            var entity = await Repository.Get(id, query => query.Include(p => p.RelatedInvoices));
            if (entity == null)
            {
                return new Response<CommisionInvoiceRes>
                {
                    StatusMessage = $"{typeof(CommisionInvoice).Name} Not found",
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            return new Response<CommisionInvoiceRes>
            {
                Data = entity.Adapt<CommisionInvoiceRes>(),
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<CommisionInvoiceRes>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public override async Task<Response<Guid>> Add(CommisionInvoiceReq reqModel)
    {
        try
        {
            var lastCommisionInvoice = await _DbContext.CommisionInvoice
                .OrderByDescending(x => x.CommissionInvoiceNumber)
                .FirstOrDefaultAsync();

            string newCommissionInvoiceNumber = lastCommisionInvoice == null
                ? "1"
                : (int.Parse(lastCommisionInvoice.CommissionInvoiceNumber) + 1).ToString("D1");

            var entity = reqModel.Adapt<CommisionInvoice>();
            entity.CommissionInvoiceNumber = newCommissionInvoiceNumber;
            entity.Id = Guid.NewGuid();
            await Repository.Add(entity);
            await UnitOfWork.SaveAsync();

            return new Response<Guid>
            {
                Data = entity.Id,
                StatusMessage = "Created successfully",
                StatusCode = HttpStatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new Response<Guid>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<CommisionInvoiceStatus> UpdateStatusAsync(Guid id, string status)
    {
        if (status == null || id == null)
        {
            throw new ArgumentException("Contract ID and Status are required.");
        }

        var validStatuses = new[] { "Approved", "UnApproved", "Prepared", "Closed", "Canceled" };
        if (!validStatuses.Contains(status))
        {
            throw new ArgumentException($"Status must be one of: {string.Join(", ", validStatuses)}");
        }

        var CommisionInvoice = await _DbContext.CommisionInvoice.Where(p => p.Id == id).FirstOrDefaultAsync();

        if (CommisionInvoice == null)
        {
            throw new KeyNotFoundException($"CommisionInvoice with ID {id} not found.");
        }

        CommisionInvoice.Status = status;
        CommisionInvoice.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
        CommisionInvoice.UpdationDate = DateTime.UtcNow;

        await UnitOfWork.SaveAsync();

        return new CommisionInvoiceStatus
        {
            Id = id,
            Status = status,
        };
    }

    public async Task<Response<IList<HistoryDataCommisionInvoiceRes>>> GetHistoryData(string? seller, string? buyer, Pagination? paginate)
    {
        try
        {
            var pagination = paginate ?? new Pagination();

            // Step 1: Get invoices with related contracts
            var baseQuery = from invoice in _DbContext.Invoices
                            where invoice.IsDeleted != true
                            from relatedContract in invoice.RelatedContracts.DefaultIfEmpty()
                            select new { invoice, relatedContract };

            // Step 2: Join with contracts and other entities
            var query = from item in baseQuery
                        join contract in _DbContext.contracts
                            on item.relatedContract != null ? item.relatedContract.ContractNumber : null equals contract.ContractNumber into contractGroup
                        from contract in contractGroup.DefaultIfEmpty()
                        join conversion in _DbContext.ConversionContractRows
                            on contract != null ? contract.Id : (Guid?)null equals conversion.ContractId into conversionGroup
                        from conversion in conversionGroup.DefaultIfEmpty()
                        join payment in _DbContext.Payments
                            on item.invoice.Id equals payment.Id into paymentGroup
                        from payment in paymentGroup.DefaultIfEmpty()
                        from relatedInvoice in payment != null ? payment.RelatedInvoices : new List<RelatedInvoice>().AsQueryable().DefaultIfEmpty()
                        where (string.IsNullOrEmpty(seller) || item.invoice.Seller == seller || (item.relatedContract != null && item.relatedContract.Seller == seller) || (contract != null && contract.Seller == seller))
                           && (string.IsNullOrEmpty(buyer) || item.invoice.Buyer == buyer || (item.relatedContract != null && item.relatedContract.Buyer == buyer) || (contract != null && contract.Buyer == buyer))
                           && (relatedInvoice == null || relatedInvoice.Balance == "0" || relatedInvoice.Balance == "0.00")
                        select new HistoryDataCommisionInvoiceRes
                        {
                            InvoiceNumber = item.invoice.InvoiceNumber,
                            InvoiceDate = item.invoice.InvoiceDate != null && DateTime.TryParse(item.invoice.InvoiceDate, out var invoiceDate) ? invoiceDate : null,
                            Buyer = item.invoice.Buyer,
                            FabricDetails = item.relatedContract != null ? item.relatedContract.FabricDetails : null,
                            InvoiceQty = item.relatedContract != null ? item.relatedContract.InvoiceQty : null,
                            Rate = item.relatedContract != null ? item.relatedContract.InvoiceRate : null,
                            InvoiceValue = item.relatedContract != null ? item.relatedContract.InvoiceValueWithGst : null,
                            CommissionPercent = conversion != null ? conversion.CommissionPercentage : null
                        };

            // Calculate total count for pagination
            var totalCount = await query.CountAsync();
            var data = await query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();

            // Update pagination metadata
            pagination.TotalCount = totalCount;
            pagination.TotalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize);

            return new Response<IList<HistoryDataCommisionInvoiceRes>>
            {
                Data = data,
                Misc = pagination,
                StatusMessage = "History data fetched successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<HistoryDataCommisionInvoiceRes>>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}*/