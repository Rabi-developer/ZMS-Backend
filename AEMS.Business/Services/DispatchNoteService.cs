using IMS.Business.DTOs.Requests;
using ZMS.Business.DTOs.Responses;
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

public interface IDispatchNoteService : IBaseService<DispatchNoteReq, DispatchNoteRes, DispatchNote>
{

   public Task<Response<DispatchNoteRes>> getBySellerBuyer(string Seller, string Buyer);
}

public class DispatchNoteService : BaseService<DispatchNoteReq, DispatchNoteRes, DispatchNoteRepository, DispatchNote>, IDispatchNoteService
{
    private readonly IDispatchNoteRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public DispatchNoteService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContext) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<DispatchNoteRepository>();
        _context = context;
        _DbContext = dbContext;
    }

    public async override Task<Response<IList<DispatchNoteRes>>> GetAll(Pagination? paginate)
    {
        try
        {
            var pagination = paginate ?? new Pagination();
            var (pag, data) = await Repository.GetAll(pagination, query => query.Include(p => p.RelatedContracts));

            return new Response<IList<DispatchNoteRes>>
            {
                Data = data.Adapt<List<DispatchNoteRes>>(),
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<DispatchNoteRes>>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public override async Task<Response<Guid>> Add(DispatchNoteReq reqModel)
    {
        try
        {
            var lastDispatchNote = await _DbContext.DispatchNotes
                .OrderByDescending(x => x.Listid)
                .FirstOrDefaultAsync();

            string newListId = lastDispatchNote == null
                ? "1"
                : (int.Parse(lastDispatchNote.Listid) + 1).ToString("D1");

            var entity = reqModel.Adapt<DispatchNote>();
            entity.Listid = newListId;

            await Repository.Add(entity);
            await UnitOfWork.SaveAsync();

            return new Response<Guid>
            {
                Data = entity.Id.Value,
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

    public async virtual Task<Response<DispatchNoteRes>> Get(Guid id)
    {
        try
        {
            var entity = await Repository.Get(id, query => query.Include(p => p.RelatedContracts));
            if (entity == null)
            {
                return new Response<DispatchNoteRes>
                {
                    StatusMessage = $"{typeof(DispatchNote).Name} Not found",
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            return new Response<DispatchNoteRes>
            {
                Data = entity.Adapt<DispatchNoteRes>(),
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<DispatchNoteRes>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<Response<DispatchNoteRes>> getBySellerBuyer(string Seller, string Buyer)
    {
        var getdata = await _DbContext.DispatchNotes
            .Where(p => p.Seller == Seller && p.Buyer == Buyer)
            .Include(p => p.RelatedContracts)
            .OrderByDescending(p => p.Id) // <-- Replace with the correct field if needed
            .FirstOrDefaultAsync();

        if (getdata == null)
        {
            return new Response<DispatchNoteRes>
            {
                StatusMessage = "Not Found",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }

        return new Response<DispatchNoteRes>
        {
            Data = getdata.Adapt<DispatchNoteRes>(),
            StatusMessage = "Fetch successfully",
            StatusCode = HttpStatusCode.OK
        };
    }

}