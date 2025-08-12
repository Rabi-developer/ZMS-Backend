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

public interface IPartyService : IBaseService<PartyReq, PartyRes, Party>
{
   Task<Response<IList<PartyRes>>> GetAll(Pagination? paginate);

}

public class PartyService : BaseService<PartyReq, PartyRes, PartyRepository, Party>, IPartyService
{
    private readonly ICommisionInvoiceRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public PartyService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<PartyRepository>();
        _context = context;
        _DbContext = dbContextn;
    }

   

    public override async Task<Response<Guid>> Add(PartyReq reqModel)
    {
        try
        {
            var lastParty = await _DbContext.Party
                .OrderByDescending(x => x.PartyNumber)
                .FirstOrDefaultAsync();

            string newPartyNumber = lastParty == null
                ? "1"
                : (int.Parse(lastParty.PartyNumber) + 1).ToString("D1");

            var entity = reqModel.Adapt<Party>();
            entity.PartyNumber = newPartyNumber;
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

}