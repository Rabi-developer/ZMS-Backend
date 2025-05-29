using IMS.Business.DTOs.Requests;
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
using ZMS.Domain.Entities;

namespace IMS.Business.Services;

public interface IInvoiceService : IBaseService<InvoiceReq, InvoiceRes, Invoice>
{
    public Task<InvoiceStatus> UpdateStatusAsync(Guid id, string status);

}

public class InvoiceService : BaseService<InvoiceReq, InvoiceRes, InvoiceRepository, Invoice>, IInvoiceService
{
    private readonly IInvoiceRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public InvoiceService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<InvoiceRepository>();
        _context = context;
        _DbContext = dbContextn;
    }


    public async override Task<Response<IList<InvoiceRes>>> GetAll(Pagination? paginate)
    {
        try
        {
            var pagination = paginate ?? new Pagination();
            //TODO: Get Pagination from the Query

            var (pag, data) = await Repository.GetAll(pagination, query => query.Include(p => p.RelatedContracts));

            return new Response<IList<InvoiceRes>>
            {
                Data = data.Adapt<List<InvoiceRes>>(),
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<InvoiceRes>>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }



    public async virtual Task<Response<InvoiceRes>> Get(Guid id)
    {
        try
        {
            var entity = await Repository.Get(id, query => query.Include(p => p.RelatedContracts));
            if (entity == null)
            {
                return new Response<InvoiceRes>
                {
                    StatusMessage = $"{typeof(Invoice).Name} Not found",
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            return new Response<InvoiceRes>
            {
                Data = entity.Adapt<InvoiceRes>(),
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<InvoiceRes>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<InvoiceStatus> UpdateStatusAsync(Guid id, string status)
    {
        if (status == null || id == null)
        {
            throw new ArgumentException("Contract ID and Status are required.");
        }

        var validStatuses = new[] { "Prepared", "Approved", "Canceled", "Closed", "UnApproved"};
        if (!validStatuses.Contains(status))
        {
            throw new ArgumentException($"Status must be one of: {string.Join(", ", validStatuses)}");
        }

        var invoice = await _DbContext.Invoices.Where(p => p.Id == id).FirstOrDefaultAsync();

        if (invoice == null)
        {
            throw new KeyNotFoundException($"Invoice with ID {id} not found.");
        }

        invoice.Status = status;
        invoice.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
        invoice.UpdationDate = DateTime.UtcNow.ToString("o");

        await UnitOfWork.SaveAsync();

        return new InvoiceStatus
        {
            Id = id,
            Status = status,
        };
    }
}