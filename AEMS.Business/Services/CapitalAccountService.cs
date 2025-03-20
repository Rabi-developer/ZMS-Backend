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

public interface ICapitalAccountService : IBaseService<CapitalAccountReq, CapitalAccountRes, CapitalAccount>
{
    Task<IList<CapitalAccount>> GetByParent (Guid ParentId);
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
                listId = (topLevelAccounts.Count + 1).ToString();
            }
            else
            {
                // If there is a parent, generate the ListId based on the parent's ListId
                var siblings = await _context.CapitalAccounts
                    .Where(p => p.ParentAccountId == reqModel.ParentAccountId)
                    .ToListAsync();
                var lastSibling = siblings.OrderByDescending(p => p.Listid).FirstOrDefault();
                if (lastSibling == null)
                {
                    listId = $"{parentAccount.Listid}.01";
                }
                else
                {
                    var lastSiblingParts = lastSibling.Listid.Split('.');
                    var lastPart = lastSiblingParts.Last();
                    var newPart = (int.Parse(lastPart) + 1).ToString("D2");
                    listId = $"{parentAccount.Listid}.{newPart}";
                }
            }

            // Map the request model to the entity
            var entity = reqModel.Adapt<CapitalAccount>();
            entity.Listid = listId;

            // Add the entity to the repository
            var ss = await Repository.Add((CapitalAccount)(entity as IMinBase ??
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
}