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
using Microsoft.IdentityModel.Tokens;
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

            var Charge = await _DbContext.Munshyana.ToListAsync();


            var result = data.Adapt<List<ChargesRes>>();

            foreach (var item in result)
            {
                foreach (var lines in item.Lines)
                {
                    if (!string.IsNullOrWhiteSpace(lines.Charge))
                        lines.Charge = Charge.FirstOrDefault(t => t.Id.ToString() == lines.Charge).ChargesDesc;
                }
            }
            return new Response<IList<ChargesRes>>
            {
                Data = result,
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



   /* public async override Task<Response<Guid>> Add(ChargesReq reqModel)
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
    }*/
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
    public async override Task<Response<ChargesRes>> Update(ChargesReq reqModel)
    {
        try
        {
            // 1. Load existing entity with related Lines
            var existingEntity = await Repository.Get((Guid)reqModel.Id, query => query.Include(p => p.Lines));
          
            if (existingEntity == null)
            {
                return new Response<ChargesRes>
                {
                    StatusMessage = "Charges not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            // 2. Update main entity properties
            existingEntity.ChargeDate = reqModel.ChargeDate;
            existingEntity.OrderNo = reqModel.OrderNo;
            existingEntity.Status = reqModel.Status;
           
            // ... update other properties

            // 3. Handle Lines collection
            var existingLineIds = existingEntity.Lines.Select(l => l.Id).ToList();
            var requestLineIds = reqModel.Lines.Where(l => !string.IsNullOrEmpty(l.Id.ToString()))
                                               .Select(l => l.Id).ToList();

            // Remove lines that are no longer in the request
            var linesToRemove = existingEntity.Lines
                .Where(l => !requestLineIds.Contains(l.Id))
                .ToList();

            foreach (var line in linesToRemove)
            {
                existingEntity.Lines.Remove(line);
            }

            // Update existing lines or add new ones
            foreach (var reqLine in reqModel.Lines)
            {
                if (!string.IsNullOrEmpty(reqLine.Id.ToString()))
                {
                    // Update existing line
                    var existingLine = existingEntity.Lines
                        .FirstOrDefault(l => l.Id == reqLine.Id);

                    if (existingLine != null)
                    {
                        existingLine.Charge = reqLine.Charge;
                        existingLine.BiltyNo = reqLine.BiltyNo;
                        existingLine.Date = reqLine.Date;
                        existingLine.Vehicle = reqLine.Vehicle;
                        existingLine.PaidTo = reqLine.PaidTo;
                        existingLine.Contact = reqLine.Contact;
                        existingLine.Remarks = reqLine.Remarks;
                        existingLine.Amount = reqLine.Amount;
                    }
                }
                else
                {
                    // Add new line
                    var newLine = reqLine.Adapt<ChargeLine>();
                    newLine.Id = Guid.NewGuid(); // Generate new ID
                    existingEntity.Lines.Add(newLine);
                }
            }

            // 4. Save changes
            await Repository.Update(existingEntity, null);
            await UnitOfWork.SaveAsync();

            return new Response<ChargesRes>
            {
                Data = existingEntity.Adapt<ChargesRes>(),
                StatusMessage = "Updated successfully",
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