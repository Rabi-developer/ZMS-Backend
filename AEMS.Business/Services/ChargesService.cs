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

public interface IChargesService : IBaseService<ChargesReq, ChargesRes, Charges>
{
    public Task<ChargesStatus> UpdateStatusAsync(Guid id, string status);

}

public class ChargesService : BaseService<ChargesReq, ChargesRes, ChargesRepository, Charges>, IChargesService
{
    private readonly IChargesRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public ChargesService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<ChargesRepository>();
        _context = context;
        _DbContext = dbContextn;
    }


    public async override Task<Response<IList<ChargesRes>>> GetAll(Pagination? paginate)
    {
        try
        {
            var pagination = paginate ?? new Pagination();
            //TODO: Get Pagination from the Query

            var (pag, data) = await Repository.GetAll(pagination, query => query.Include(p => p.Lines));

            return new Response<IList<ChargesRes>>
            {
                Data = data.Adapt<List<ChargesRes>>(),
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<ChargesRes>>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }


    public async override Task<Response<Guid>> Add(ChargesReq reqModel)
    {
        try
        {
            var entity = reqModel.Adapt<Charges>();

            var GetlastNo = await UnitOfWork._context.Charges
     .OrderByDescending(p => p.Id)
     .FirstOrDefaultAsync();

            if (GetlastNo == null || GetlastNo.ChargeNo == "")
            {
                entity.ChargeNo = "1";
            }
            else
            {
                int NewNo = int.Parse(GetlastNo.ChargeNo) + 1;
                entity.ChargeNo = NewNo.ToString();
            }

            var ss = await Repository.Add((Charges)(entity as IMinBase ??
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
    public async virtual Task<Response<ChargesRes>> Get(Guid id)
    {
        try
        {
            var entity = await Repository.Get(id, query => query.Include(p => p.Lines));
            if (entity == null)
            {
                return new Response<ChargesRes>
                {
                    StatusMessage = $"{typeof(Charges).Name} Not found",
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            return new Response<ChargesRes>
            {
                Data = entity.Adapt<ChargesRes>(),
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ChargesRes>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<ChargesStatus> UpdateStatusAsync(Guid id, string status)
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

        var Charges = await _DbContext.Charges.Where(p => p.Id == id).FirstOrDefaultAsync();

        if (Charges == null)
        {
            throw new KeyNotFoundException($"Charges with ID {id} not found.");
        }

        Charges.Status = status;
        Charges.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
        Charges.UpdationDate = DateTime.UtcNow.ToString("o");

        await UnitOfWork.SaveAsync();

        return new ChargesStatus
        {
            Id = id,
            Status = status,
        };
    }
}