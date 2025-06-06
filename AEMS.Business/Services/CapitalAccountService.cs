﻿using IMS.Business.DTOs.Requests;
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

public interface ICapitalAccountService : IBaseService<CapitalAccountReq, CapitalAccountRes, CapitalAccount>
{
    Task<IList<CapitalAccount>> GetByParent (Guid ParentId);
    Task<Response<IList<CapitalAccountRes>>> GetAllHierarchy();

}
public class CapitalAccountService : BaseService<CapitalAccountReq, CapitalAccountRes, CapitalAccountRepository, CapitalAccount>, ICapitalAccountService
{
    private readonly ApplicationDbContext _context;
    private readonly ICapitalAccountRepository _repository;
    protected readonly IUnitOfWork UnitOfWork;
  

    public CapitalAccountService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork)
    {
        _context = dbContext;
        UnitOfWork = unitOfWork;
        _repository = UnitOfWork.GetRepository<CapitalAccountRepository>();
       
    }

    

    public async Task<IList<CapitalAccount>> GetByParent(Guid ParentId)
    {
        return await Repository.GetByParent(ParentId);
    }

    public async override Task<Response<Guid>> Add(CapitalAccountReq reqModel)
    {
        try
        {
            // Fetch the parent account if ParentAccountId is provided
            CapitalAccount parentAccount = null;
            if (reqModel.ParentAccountId.HasValue)
            {
                parentAccount = await _context.CapitalAccounts
                    .FirstOrDefaultAsync(p => p.Id == reqModel.ParentAccountId.Value);
            }

            // Generate the ListId based on the parent's ListId
            string listId;
            if (parentAccount == null)
            {
                // If no parent, this is a top-level account
                var topLevelAccounts = await _context.CapitalAccounts
                    .Where(p => p.ParentAccountId == null)
                    .ToListAsync();
                listId = (topLevelAccounts.Count + 1).ToString(); // Top-level: 1, 2, 3, etc.
            }
            else
            {
                // If there is a parent, generate the ListId based on the parent's ListId
                var siblings = await _context.CapitalAccounts
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
                        case 1: // Parent is top-level (e.g., "1")
                            listId = $"{parentAccount.Listid}.01"; // First child: "1.01"
                            break;
                        case 2: // Parent is first child (e.g., "1.01")
                            listId = $"{parentAccount.Listid}.01"; // First sub-child: "1.01.01"
                            break;
                        case 3: // Parent is sub-child (e.g., "1.01.01")
                            listId = $"{parentAccount.Listid}.001"; // First sub-sub-child: "1.01.01.001"
                            break;
                        case 4: // Parent is sub-sub-child (e.g., "1.01.01.001")
                            listId = $"{parentAccount.Listid}.0001"; // First sub-sub-sub-child: "1.01.01.001.0001"
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
                        case 1: // Parent is top-level (e.g., "1")
                            listId = $"{parentAccount.Listid}.{(int.Parse(lastPart) + 1):D2}"; // Increment: "1.02", "1.03", etc.
                            break;
                        case 2: // Parent is first child (e.g., "1.01")
                            listId = $"{parentAccount.Listid}.{(int.Parse(lastPart) + 1):D2}"; // Increment: "1.01.02", "1.01.03", etc.
                            break;
                        case 3: // Parent is sub-child (e.g., "1.01.01")
                            listId = $"{parentAccount.Listid}.{(int.Parse(lastPart) + 1):D3}"; // Increment: "1.01.01.002", "1.01.01.003", etc.
                            break;
                        case 4: // Parent is sub-sub-child (e.g., "1.01.01.001")
                            listId = $"{parentAccount.Listid}.{(int.Parse(lastPart) + 1):D4}"; // Increment: "1.01.01.001.0002", "1.01.01.001.0003", etc.
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
            var entity = reqModel.Adapt<CapitalAccount>();
            entity.Listid = listId;

            // Add the entity to the repository
            var ss = await Repository.Add((CapitalAccount)(entity as IMinBase ??
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

    public override async Task<Response<IList<CapitalAccountRes>>> GetAll(Pagination? paginate)
    {
        try
        {
            var pagination = paginate ?? new Pagination();

            // Include CapitalAccountProducts in the query
              var (pag, data) = await Repository.GetAll(
             pagination,
             query => query.Include(x => x.ParentAccount)
             .AsNoTracking()
             .OrderBy(x => x.Id)
 );

            Console.WriteLine($"Fetched {data.Count} records");

            if (!data.Any())
            {
                return new Response<IList<CapitalAccountRes>>
                {
                    Data = new List<CapitalAccountRes>(),
                    Misc = pag,
                    StatusMessage = "No records found",
                    StatusCode = HttpStatusCode.NoContent
                };
            }


            // Map the data to the response DTO
            var CapitalAccountResList = data.Adapt<List<CapitalAccountRes>>();

            //// Manually map CapitalAccountProducts to Products in the response
            //foreach (var CapitalAccountRes in CapitalAccountResList)
            //{
            //    var CapitalAccount = data.FirstOrDefault(t => t.Id == CapitalAccountRes.Id);
            //    if (CapitalAccount != null)
            //    {
            //        CapitalAccountRes.Products = CapitalAccount.CapitalAccountProducts
            //            .Select(tp => new CapitalAccountProductRes
            //            {
            //                ProductId = tp.ProductId,
            //                Quantity = tp.Quantity,
            //                Condition = tp.Condition
            //            })
            //            .ToList();
            //    }
            //}

            return new Response<IList<CapitalAccountRes>>
            {
                Data = CapitalAccountResList,
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<CapitalAccountRes>>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<Response<IList<CapitalAccountRes>>> GetAllHierarchy()
    {
        try
        {
            var hierarchy = await _repository.GetAllHierarchy();
            return MapHierarchyResponse(hierarchy);
        }
        catch (Exception ex)
        {
            return new Response<IList<CapitalAccountRes>>
            {
                StatusMessage = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    private Response<IList<CapitalAccountRes>> MapHierarchyResponse(IList<CapitalAccount> hierarchy)
    {
        CapitalAccountRes MapAccount(CapitalAccount account)
        {
            return new CapitalAccountRes
            {
                Id = account.Id,
                Listid = account.Listid,
                Description = account.Description,
                ParentAccountId = account.ParentAccountId,

                Children = account.Children
                    ?.OrderBy(c => c.Listid)
                    .Select(MapAccount)
                    .ToList()
                    ?? new List<CapitalAccountRes>()
            };
        }

        var result = hierarchy
            .OrderBy(h => h.Listid)  
            .Select(MapAccount)
            .ToList();

        return new Response<IList<CapitalAccountRes>>
        {
            Data = result,
            StatusMessage = "Hierarchy fetched successfully",
            StatusCode = HttpStatusCode.OK
        };
    }
}