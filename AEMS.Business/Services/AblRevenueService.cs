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
using System.Net;
using ZMS.Business.DTOs.Requests;
using ZMS.Business.DTOs.Responses;

namespace IMS.Business.Services;

public interface IAblRevenueService : IBaseService<AblRevenueReq, AblRevenueRes, AblRevenue>
{
    Task<IList<AblRevenue>> GetByParent(Guid ParentId);
}
public class AblRevenueService : BaseService<AblRevenueReq, AblRevenueRes, AblRevenueRepository, AblRevenue>, IAblRevenueService
{
    private readonly ApplicationDbContext _context;
    private readonly IAblRevenueRepository _repository;
    protected readonly IUnitOfWork UnitOfWork;


    public AblRevenueService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork)
    {
        _context = dbContext;
        UnitOfWork = unitOfWork;
        _repository = UnitOfWork.GetRepository<AblRevenueRepository>();

    }



    public async Task<IList<AblRevenue>> GetByParent(Guid ParentId)
    {
        return await Repository.GetByParent(ParentId);
    }

    public override async Task<Response<IList<AblRevenueRes>>> GetAllByUser(Pagination pagination, Guid userId, bool onlyusers = true)
    {
        try
        {
            var (pag, data) = await Repository.GetAllByUser(pagination, onlyusers, userId);

            // Manually map to prevent circular references
            var res = data.Select(item => new AblRevenueRes
            {
                Id = item.Id,
                Listid = item.Listid,
                Description = item.Description,
                ParentAccountId = item.ParentAccountId,
                DueDate = item.DueDate,
                FixedAmount = item.FixedAmount,
                Paid = item.Paid,
                CreatedBy = item.CreatedBy,
                CreatedDateTime = item.CreatedDateTime,
                ModifiedBy = item.ModifiedBy,
                ModifiedDateTime = item.ModifiedDateTime,
                IsActive = item.IsActive,
                IsDeleted = item.IsDeleted,
                // Don't load navigation properties here to avoid circular references
                ParentAccount = null,
                Children = null
            }).ToList();

            return new Response<IList<AblRevenueRes>>
            {
                Data = res,
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<AblRevenueRes>>
            {
                StatusMessage = e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async override Task<Response<Guid>> Add(AblRevenueReq reqModel)
    {
        try
        {
            // Fetch the parent account if ParentAccountId is provided
            AblRevenue parentAccount = null;
            if (reqModel.ParentAccountId.HasValue)
            {
                parentAccount = await _context.AblRevenue
                    .FirstOrDefaultAsync(p => p.Id == reqModel.ParentAccountId.Value);
            }

            // Generate the ListId based on the parent's ListId
            string listId;
            if (parentAccount == null)
            {
                // If no parent, this is a top-level account
                // Hardcode the top-level ListId to "2"
                listId = "5";
            }
            else
            {
                // If there is a parent, generate the ListId based on the parent's ListId
                var siblings = await _context.AblRevenue
                    .Where(p => p.ParentAccountId == reqModel.ParentAccountId)
                    .ToListAsync();

                // Determine the depth of the hierarchy
                var parentListIdParts = parentAccount.Listid.Split('.');
                int depth = parentListIdParts.Length;

                // Generate the next ListId based on the depth
                if (siblings.Count == 0)
                {
                    // First child at this level
                    switch (depth)
                    {
                        case 1: // Parent is top-level (e.g., "2")
                            listId = $"{parentAccount.Listid}.01"; // First child: "2.01"
                            break;
                        case 2: // Parent is first child (e.g., "2.01")
                            listId = $"{parentAccount.Listid}.01"; // First sub-child: "2.01.01"
                            break;
                        case 3: // Parent is sub-child (e.g., "2.01.01")
                            listId = $"{parentAccount.Listid}.001"; // First sub-sub-child: "2.01.01.001"
                            break;
                        case 4: // Parent is sub-sub-child (e.g., "2.01.01.001")
                            listId = $"{parentAccount.Listid}.0001"; // First sub-sub-sub-child: "2.01.01.001.0001"
                            break;
                        default:
                            // For deeper levels, add one more zero
                            listId = $"{parentAccount.Listid}.{new string('0', depth - 2)}1";
                            break;
                    }
                }
                else
                {
                    // Not the first child at this level
                    var lastSibling = siblings.OrderByDescending(p => p.Listid).FirstOrDefault();
                    var lastSiblingParts = lastSibling.Listid.Split('.');
                    var lastPart = lastSiblingParts.Last();

                    switch (depth)
                    {
                        case 1: // Parent is top-level (e.g., "2")
                            listId = $"{parentAccount.Listid}.{(int.Parse(lastPart) + 1):D2}"; // Increment: "2.02", "2.03", etc.
                            break;
                        case 2: // Parent is first child (e.g., "2.01")
                            listId = $"{parentAccount.Listid}.{(int.Parse(lastPart) + 1):D2}"; // Increment: "2.01.02", "2.01.03", etc.
                            break;
                        case 3: // Parent is sub-child (e.g., "2.01.01")
                            listId = $"{parentAccount.Listid}.{(int.Parse(lastPart) + 1):D3}"; // Increment: "2.01.01.002", "2.01.01.003", etc.
                            break;
                        case 4: // Parent is sub-sub-child (e.g., "2.01.01.001")
                            listId = $"{parentAccount.Listid}.{(int.Parse(lastPart) + 1):D4}"; // Increment: "2.01.01.001.0002", "2.01.01.001.0003", etc.
                            break;
                        default:
                            // For deeper levels, add one more zero
                            int zeros = depth - 2;
                            listId = $"{parentAccount.Listid}.{(int.Parse(lastPart) + 1).ToString(new string('0', zeros) + "1")}";
                            break;
                    }
                }
            }

            // Map the request model to the entity
            var entity = reqModel.Adapt<AblRevenue>();
            entity.Listid = listId;

            // Add the entity to the repository
            var ss = await Repository.Add((AblRevenue)(entity as IMinBase ??
            throw new InvalidOperationException("Conversion to IMinBase Failed. Make sure there's Id and CreatedDate properties.")));
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
    public override async Task<Response<IList<AblRevenueRes>>> GetAll(Pagination? paginate)
    {
        try
        {
            var pagination = paginate ?? new Pagination();

            // Get data WITHOUT navigation properties to prevent circular references
            var (pag, data) = await Repository.GetAll(
               pagination,
               query => query.AsNoTracking()
                           .OrderBy(x => x.Id)
            );

            Console.WriteLine($"Fetched {data.Count} records");

            if (!data.Any())
            {
                return new Response<IList<AblRevenueRes>>
                {
                    Data = new List<AblRevenueRes>(),
                    Misc = pag,
                    StatusMessage = "No records found",
                    StatusCode = HttpStatusCode.NoContent
                };
            }

            // Manually map to prevent circular references
            var AblRevenueResList = data.Select(item => new AblRevenueRes
            {
                Id = item.Id,
                Listid = item.Listid,
                Description = item.Description,
                ParentAccountId = item.ParentAccountId,
                DueDate = item.DueDate,
                FixedAmount = item.FixedAmount,
                Paid = item.Paid,
                CreatedBy = item.CreatedBy,
                CreatedDateTime = item.CreatedDateTime,
                ModifiedBy = item.ModifiedBy,
                ModifiedDateTime = item.ModifiedDateTime,
                IsActive = item.IsActive,
                IsDeleted = item.IsDeleted,
                // Don't load navigation properties here to avoid circular references
                ParentAccount = null,
                Children = null
            }).ToList();

            return new Response<IList<AblRevenueRes>>
            {
                Data = AblRevenueResList,
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<AblRevenueRes>>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}