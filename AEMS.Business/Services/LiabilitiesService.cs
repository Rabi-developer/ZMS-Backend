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

namespace IMS.Business.Services;

public interface ILiabilitiesService : IBaseService<LiabilitiesReq, LiabilitiesRes, Liabilities>
{
    Task<IList<Liabilities>> GetByParent(Guid ParentId);
}
public class LiabilitiesService : BaseService<LiabilitiesReq, LiabilitiesRes, LiabilitiesRepository, Liabilities>, ILiabilitiesService
{
    private readonly ApplicationDbContext _context;
    private readonly ILiabilitiesRepository _repository;
    protected readonly IUnitOfWork UnitOfWork;


    public LiabilitiesService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork)
    {
        _context = dbContext;
        UnitOfWork = unitOfWork;
        _repository = UnitOfWork.GetRepository<LiabilitiesRepository>();

    }



    public async Task<IList<Liabilities>> GetByParent(Guid ParentId)
    {
        return await Repository.GetByParent(ParentId);
    }

    public async override Task<Response<Guid>> Add(LiabilitiesReq reqModel)
    {
        try
        {
            // Fetch the parent account if ParentAccountId is provided
            Liabilities parentAccount = null;
            if (reqModel.ParentAccountId.HasValue)
            {
                parentAccount = await _context.Liabilities
                    .FirstOrDefaultAsync(p => p.Id == reqModel.ParentAccountId.Value);
            }

            // Generate the ListId based on the parent's ListId
            string listId;
            if (parentAccount == null)
            {
                // If no parent, this is a top-level account
                // Hardcode the top-level ListId to "2"
                listId = "2";
            }
            else
            {
                // If there is a parent, generate the ListId based on the parent's ListId
                var siblings = await _context.Liabilities
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
            var entity = reqModel.Adapt<Liabilities>();
            entity.Listid = listId;

            // Add the entity to the repository
            var ss = await Repository.Add((Liabilities)(entity as IMinBase ??
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
    public override async Task<Response<IList<LiabilitiesRes>>> GetAll(Pagination? paginate)
    {
        try
        {
            var pagination = paginate ?? new Pagination();

            // Include LiabilitiesProducts in the query
            var (pag, data) = await Repository.GetAll(
           pagination,
           query => query.Include(x => x.ParentAccount)
           .AsNoTracking()
           .OrderBy(x => x.Id)
);

            Console.WriteLine($"Fetched {data.Count} records");

            if (!data.Any())
            {
                return new Response<IList<LiabilitiesRes>>
                {
                    Data = new List<LiabilitiesRes>(),
                    Misc = pag,
                    StatusMessage = "No records found",
                    StatusCode = HttpStatusCode.NoContent
                };
            }


            // Map the data to the response DTO
            var LiabilitiesResList = data.Adapt<List<LiabilitiesRes>>();

            //// Manually map LiabilitiesProducts to Products in the response
            //foreach (var LiabilitiesRes in LiabilitiesResList)
            //{
            //    var Liabilities = data.FirstOrDefault(t => t.Id == LiabilitiesRes.Id);
            //    if (Liabilities != null)
            //    {
            //        LiabilitiesRes.Products = Liabilities.LiabilitiesProducts
            //            .Select(tp => new LiabilitiesProductRes
            //            {
            //                ProductId = tp.ProductId,
            //                Quantity = tp.Quantity,
            //                Condition = tp.Condition
            //            })
            //            .ToList();
            //    }
            //}

            return new Response<IList<LiabilitiesRes>>
            {
                Data = LiabilitiesResList,
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<LiabilitiesRes>>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}