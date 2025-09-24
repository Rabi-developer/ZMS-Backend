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

public interface IReceiptService : IBaseService<ReceiptReq, ReceiptRes, Receipt>
{
    public Task<ReceiptStatus> UpdateStatusAsync(Guid id, string status);

}

public class ReceiptService : BaseService<ReceiptReq, ReceiptRes, ReceiptRepository, Receipt>, IReceiptService
{
    private readonly IReceiptRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public ReceiptService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<ReceiptRepository>();
        _context = context;
        _DbContext = dbContextn;
    }


    public async override Task<Response<IList<ReceiptRes>>> GetAll(Pagination? paginate)
    {
        try
        {
            var pagination = paginate ?? new Pagination();
            //TODO: Get Pagination from the Query

            var (pag, data) = await Repository.GetAll(pagination, query => query.Include(p => p.Items));

            var Party = await _DbContext.Party.ToListAsync();

            var result = data.Adapt<List<ReceiptRes>>();

            foreach (var item in result)
            {
                if (!string.IsNullOrWhiteSpace(item.Party))
                    item.Party = Party.FirstOrDefault(t => t.Id.ToString() == item.Party)?.Name;
            }
            return new Response<IList<ReceiptRes>>
            {
                Data = result,
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<ReceiptRes>>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async override Task<Response<Guid>> Add(ReceiptReq reqModel)
    {
        try
        {
            var entity = reqModel.Adapt<Receipt>();

            var GetlastNo = await UnitOfWork._context.Receipt
            .OrderByDescending(p => p.Id)
            .FirstOrDefaultAsync();

            if (GetlastNo.ReceiptNo == null || GetlastNo.ReceiptNo == "REC1758707358926542")
            {
                GetlastNo.ReceiptNo = "0";
            }
            
            else
            {
                int NewNo = int.Parse(GetlastNo.ReceiptNo) + 1;
                entity.ReceiptNo = NewNo.ToString();
            }

            var ss = await Repository.Add((Receipt)(entity as IMinBase ??
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

    public async virtual Task<Response<ReceiptRes>> Get(Guid id)
    {
        try
        {
            var entity = await Repository.Get(id, query => query.Include(p => p.Items));
            if (entity == null)
            {
                return new Response<ReceiptRes>
                {
                    StatusMessage = $"{typeof(Receipt).Name} Not found",
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            return new Response<ReceiptRes>
            {
                Data = entity.Adapt<ReceiptRes>(),
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ReceiptRes>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<ReceiptStatus> UpdateStatusAsync(Guid id, string status)
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

        var Receipt = await _DbContext.Receipt.Where(p => p.Id == id).FirstOrDefaultAsync();

        if (Receipt == null)
        {
            throw new KeyNotFoundException($"Receipt with ID {id} not found.");
        }

        Receipt.Status = status;
        Receipt.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
        Receipt.UpdationDate = DateTime.UtcNow.ToString("o");

        await UnitOfWork.SaveAsync();

        return new ReceiptStatus
        {
            Id = id,
            Status = status,
        };
    }
}