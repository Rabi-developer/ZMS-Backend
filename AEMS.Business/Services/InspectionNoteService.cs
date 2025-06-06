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

public interface IInspectionNoteService : IBaseService<InspectionNoteReq, InspectionNoteRes, InspectionNote>
{
    public Task<InspectionNoteStatus> UpdateStatusAsync(Guid id, string status);

}

public class InspectionNoteService : BaseService<InspectionNoteReq, InspectionNoteRes, InspectionNoteRepository, InspectionNote>, IInspectionNoteService
{
    private readonly IInspectionNoteRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public InspectionNoteService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<InspectionNoteRepository>();
        _context = context;
        _DbContext = dbContextn;
    }


    public async override Task<Response<IList<InspectionNoteRes>>> GetAll(Pagination? paginate)
    {
        try
        {
            var pagination = paginate ?? new Pagination();
            //TODO: Get Pagination from the Query

            var (pag, data) = await Repository.GetAll(pagination, query => query.Include(p => p.RelatedContracts));

            return new Response<IList<InspectionNoteRes>>
            {
                Data = data.Adapt<List<InspectionNoteRes>>(),
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<InspectionNoteRes>>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }



    public async virtual Task<Response<InspectionNoteRes>> Get(Guid id)
    {
        try
        {
            var entity = await Repository.Get(id, query => query.Include(p => p.RelatedContracts));
            if (entity == null)
            {
                return new Response<InspectionNoteRes>
                {
                    StatusMessage = $"{typeof(InspectionNote).Name} Not found",
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            return new Response<InspectionNoteRes>
            {
                Data = entity.Adapt<InspectionNoteRes>(),
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<InspectionNoteRes>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<InspectionNoteStatus> UpdateStatusAsync(Guid id, string status)
    {
        if (status == null || id == null)
        {
            throw new ArgumentException("Contract ID and Status are required.");
        }

        var validStatuses = new[] { "Approved Inspection" };
        if (!validStatuses.Contains(status))
        {
            throw new ArgumentException($"Status must be one of: {string.Join(", ", validStatuses)}");
        }

        var InspectionNote = await _DbContext.InspectionNotes.Where(p => p.Id == id).FirstOrDefaultAsync();

        if (InspectionNote == null)
        {
            throw new KeyNotFoundException($"InspectionNote with ID {id} not found.");
        }

        InspectionNote.Status = status;
        InspectionNote.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
        InspectionNote.UpdationDate = DateTime.UtcNow.ToString("o");

        await UnitOfWork.SaveAsync();

        return new InspectionNoteStatus
        {
            Id = id,
            Status = status,
        };
    }
}