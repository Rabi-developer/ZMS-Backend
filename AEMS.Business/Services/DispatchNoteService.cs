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
using IMS.Domain.Base;

namespace IMS.Business.Services;

public interface IDispatchNoteService : IBaseService<DispatchNoteReq, DispatchNoteRes, DispatchNote>
{

   public Task<Response<DispatchNoteRes>> getBySellerBuyer(string Seller, string Buyer);
    public Task<DispatchNoteStatus> UpdateStatusAsync(Guid id, string status);
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
            entity.Id = Guid.NewGuid();
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

    public async override Task<Response<DispatchNoteRes>> Update(DispatchNoteReq reqModel)
    {
        try
        {
            // Map the incoming DTO to the DispatchNote entity
            var entity = reqModel.Adapt<DispatchNote>();

            // Fetch the existing entity with RelatedContracts
            var existingEntity = await Repository.Get(entity.Id.Value, query => query.Include(p => p.RelatedContracts));
            if (existingEntity == null)
            {
                return new Response<DispatchNoteRes>
                {
                    StatusMessage = $"{typeof(DispatchNote).Name} not found with Id: {entity.Id}",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            // Update scalar properties
            _DbContext.Entry(existingEntity).CurrentValues.SetValues(entity);

            // Handle RelatedContracts
            existingEntity.RelatedContracts ??= new List<RelatedContract>();

            // Get IDs of existing and incoming contracts
            var existingContractIds = existingEntity.RelatedContracts.Select(rc => rc.Id).ToList();
            var incomingContractIds = entity.RelatedContracts?.Select(rc => rc.Id).Where(id => id.HasValue).Select(id => id.Value).ToList() ?? new List<Guid>();

            // Remove contracts that are no longer in the incoming list
            existingEntity.RelatedContracts.RemoveAll(rc => !incomingContractIds.Contains(rc.Id.Value));

            // Add or update contracts
            if (entity.RelatedContracts != null)
            {
                foreach (var incomingContract in entity.RelatedContracts.Where(rc => rc.Id.HasValue))
                {
                    var existingContract = existingEntity.RelatedContracts.FirstOrDefault(rc => rc.Id == incomingContract.Id);
                    if (existingContract == null)
                    {
                        // Add new contract
                        var newContract = incomingContract.Adapt<RelatedContract>();
                        // Ensure the new contract is not tracked
                        _DbContext.Entry(newContract).State = EntityState.Detached;
                        existingEntity.RelatedContracts.Add(newContract);
                    }
                    else
                    {
                        // Update existing contract
                        _DbContext.Entry(existingContract).CurrentValues.SetValues(incomingContract);
                    }
                }
            }

            // Update ModifiedDateTime
            existingEntity.ModifiedDateTime = DateTime.UtcNow;

            // Save changes
            await UnitOfWork.SaveAsync();

            return new Response<DispatchNoteRes>
            {
                Data = existingEntity.Adapt<DispatchNoteRes>(),
                StatusMessage = "Updated successfully",
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

    public async Task<DispatchNoteStatus> UpdateStatusAsync(Guid id, string status)
    {
        if (status == null || id == null)
        {
            throw new ArgumentException("Dispatcht ID and Status are required.");
        }

        var validStatuses = new[] { "Pending", "Approved", "Canceled", "Closed Dispatch", "Closed Payment", "Complete Closed", };
        if (!validStatuses.Contains(status))
        {
            throw new ArgumentException($"Status must be one of: {string.Join(", ", validStatuses)}");
        }

        var dispatchnote = await _DbContext.DispatchNotes.Where(p => p.Id == id).FirstOrDefaultAsync();

        if (dispatchnote == null)
        {
            throw new KeyNotFoundException($"Dispatch with ID {id} not found.");
        }

        dispatchnote.Status = status;
        dispatchnote.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
        dispatchnote.UpdationDate = DateTime.UtcNow.ToString("o");

        await UnitOfWork.SaveAsync();

        return new DispatchNoteStatus
        {
            Id = id,
            Status = status,
        };
    }

}