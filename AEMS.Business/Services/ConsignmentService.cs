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

public interface IConsignmentService : IBaseService<ConsignmentReq, ConsignmentRes, Consignment>
{
    public Task<ConsignmentStatus> UpdateStatusAsync(Guid id, string status);

}

public class ConsignmentService : BaseService<ConsignmentReq, ConsignmentRes, ConsignmentRepository, Consignment>, IConsignmentService
{
    private readonly IConsignmentRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public ConsignmentService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<ConsignmentRepository>();
        _context = context;
        _DbContext = dbContextn;
    }


    public async override Task<Response<IList<ConsignmentRes>>> GetAll(Pagination? paginate)
    {
        try
        {
            var pagination = paginate ?? new Pagination();
            //TODO: Get Pagination from the Query

            var (pag, data) = await Repository.GetAll(pagination, query => query.Include(p => p.Items));

            return new Response<IList<ConsignmentRes>>
            {
                Data = data.Adapt<List<ConsignmentRes>>(),
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<ConsignmentRes>>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }



    public async virtual Task<Response<ConsignmentRes>> Get(Guid id)
    {
        try
        {
            var entity = await Repository.Get(id, query => query.Include(p => p.Items));
            if (entity == null)
            {
                return new Response<ConsignmentRes>
                {
                    StatusMessage = $"{typeof(Consignment).Name} Not found",
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            return new Response<ConsignmentRes>
            {
                Data = entity.Adapt<ConsignmentRes>(),
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ConsignmentRes>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
    public async override Task<Response<Guid>> Add(ConsignmentReq reqModel)
    {
        try
        {
            var entity = reqModel.Adapt<Consignment>();

            var GetlastNo = await UnitOfWork._context.Consignment
     .OrderByDescending(p => p.Id)
     .FirstOrDefaultAsync();

            if (GetlastNo == null || GetlastNo.ReceiptNo == "")
            {
                entity.ReceiptNo = "1";
            }
            else
            {
                int NewNo = int.Parse(GetlastNo.ReceiptNo) + 1;
                entity.ReceiptNo = NewNo.ToString();
            }

            var ss = await Repository.Add((Consignment)(entity as IMinBase ??
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

    public async Task<ConsignmentStatus> UpdateStatusAsync(Guid id, string status)
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

        var Consignment = await _DbContext.Consignment.Where(p => p.Id == id).FirstOrDefaultAsync();

        if (Consignment == null)
        {
            throw new KeyNotFoundException($"Consignment with ID {id} not found.");
        }

        Consignment.Status = status;
        Consignment.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
        Consignment.UpdationDate = DateTime.UtcNow.ToString("o");

        await UnitOfWork.SaveAsync();

        return new ConsignmentStatus
        {
            Id = id,
            Status = status,
        };
    }
}