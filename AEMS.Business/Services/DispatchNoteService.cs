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

public interface IDispatchNoteService : IBaseService<DispatchNoteReq, DispatchNoteRes, DispatchNote>
{
}

public class DispatchNoteService : BaseService<DispatchNoteReq, DispatchNoteRes, DispatchNoteRepository, DispatchNote>, IDispatchNoteService
{
    private readonly IDispatchNoteRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public DispatchNoteService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<DispatchNoteRepository>();
        _context = context;
        _DbContext = dbContextn;
    }


    public async override Task<Response<IList<DispatchNoteRes>>> GetAll(Pagination? paginate)
    {
        try
        {
            var pagination = paginate ?? new Pagination();
            //TODO: Get Pagination from the Query

            var (pag, data) = await Repository.GetAll(pagination, query=> query.Include(p=> p.RelatedContracts));
           
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


}