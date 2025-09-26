using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Base;
using IMS.Domain.Context;
using IMS.Domain.Entities;
using IMS.Domain.Utilities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;
using ZMS.Business.DTOs.Requests;
using ZMS.Domain.Entities;

namespace IMS.Business.Services;

public interface IBiltyPaymentInvoiceService : IBaseService<BiltyPaymentInvoiceReq, BiltyPaymentInvoiceRes, BiltyPaymentInvoice>
{
    public Task<BillPaymentInvoicesStatus> UpdateStatusAsync(Guid id, string status);

}

public class BiltyPaymentInvoiceService : BaseService<BiltyPaymentInvoiceReq, BiltyPaymentInvoiceRes, BiltyPaymentInvoiceRepository, BiltyPaymentInvoice>, IBiltyPaymentInvoiceService
{
    private readonly IBiltyPaymentInvoiceRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public BiltyPaymentInvoiceService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<BiltyPaymentInvoiceRepository>();
        _context = context;
        _DbContext = dbContextn;
    }


    public async override Task<Response<IList<BiltyPaymentInvoiceRes>>> GetAll(Pagination? paginate)
    {
        try
        {
            var pagination = paginate ?? new Pagination();
            //TODO: Get Pagination from the Query

            var (pag, data) = await Repository.GetAll(pagination, query => query.Include(p => p.Lines));

            /*var OrderNo = await _DbContext.BookingOrder.ToListAsync();*/
            var Broker = await _DbContext.Brooker.ToListAsync();


            var result = data.Adapt<List<BiltyPaymentInvoiceRes>>();

            foreach (var item in result)
            {
                foreach (var lines in item.Lines)
                {
                    if (!string.IsNullOrWhiteSpace(lines.Broker))
                        lines.Broker = Broker.FirstOrDefault(t => t.Id.ToString() == lines.Broker).Name;
                }
            }
            return new Response<IList<BiltyPaymentInvoiceRes>>
            {
                Data = result,
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<BiltyPaymentInvoiceRes>>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }



    public async override Task<Response<Guid>> Add(BiltyPaymentInvoiceReq reqModel)
    {
        try
        {
            var entity = reqModel.Adapt<BiltyPaymentInvoice>();

            var GetlastNo = await UnitOfWork._context.BiltyPaymentInvoice
           .OrderByDescending(p => p.Id)
           .FirstOrDefaultAsync();
            if (GetlastNo.InvoiceNo == null || GetlastNo.InvoiceNo == "NV1758801917652188")
            {
                GetlastNo.InvoiceNo = "0";
            }
            if (GetlastNo == null || GetlastNo.InvoiceNo == "")
            {
                entity.InvoiceNo = "1";
            }
            else
            {
                int NewNo = int.Parse(GetlastNo.InvoiceNo) + 1;
                entity.InvoiceNo = NewNo.ToString();
            }

            var ss = await Repository.Add((BiltyPaymentInvoice)(entity as IMinBase ??
             throw new InvalidOperationException(
             "Conversion to IMinBase Failed. Make sure there's Id and CreatedDate properties.")));
            await UnitOfWork.SaveAsync();
            return new Response<Guid>
            {

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
    public async virtual Task<Response<BiltyPaymentInvoiceRes>> Get(Guid id)
    {
        try
        {
            var entity = await Repository.Get(id, query => query.Include(p => p.Lines));
            if (entity == null)
            {
                return new Response<BiltyPaymentInvoiceRes>
                {
                    StatusMessage = $"{typeof(BiltyPaymentInvoice).Name} Not found",
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            return new Response<BiltyPaymentInvoiceRes>
            {
                Data = entity.Adapt<BiltyPaymentInvoiceRes>(),
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<BiltyPaymentInvoiceRes>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<BillPaymentInvoicesStatus> UpdateStatusAsync(Guid id, string status)
    {
        if (status == null || id == null)
        {
            throw new ArgumentException("Contract ID and Status are required.");
        }

        var validStatuses = new[] { "Prepared", "Approved", "Canceled", "Closed", "UnApproved" };
        if (!validStatuses.Contains(status))
        {
            throw new ArgumentException($"Status must be one of: {string.Join(", ", validStatuses)}");
        }

        var BiltyPaymentInvoice = await _DbContext.BiltyPaymentInvoice.Where(p => p.Id == id).FirstOrDefaultAsync();

        if (BiltyPaymentInvoice == null)
        {
            throw new KeyNotFoundException($"BiltyPaymentInvoice with ID {id} not found.");
        }

        BiltyPaymentInvoice.Status = status;
        BiltyPaymentInvoice.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
        BiltyPaymentInvoice.UpdationDate = DateTime.UtcNow.ToString("o");

        await UnitOfWork.SaveAsync();

        return new BillPaymentInvoicesStatus
        {
            Id = id,
            Status = status,
        };
    }
}