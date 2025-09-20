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

public interface IBrookerService : IBaseService<BrookerReq, BrookerRes, Brooker>
{
    Task<Response<IList<BrookerRes>>> GetAll(Pagination? paginate);

}

public class BrookerService : BaseService<BrookerReq, BrookerRes, BrookerRepository, Brooker>, IBrookerService
{
    private readonly IBrookerRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public BrookerService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = (IBrookerRepository?)UnitOfWork.GetRepository<BrookerRepository>();
        _context = context;
        _DbContext = dbContextn;
    }



    public override async Task<Response<Guid>> Add(BrookerReq reqModel)
    {
        try
        {
            var lastBrooker = await _DbContext.Brooker
                .OrderByDescending(x => x.BrookerNumber)
                .FirstOrDefaultAsync();
            if (lastBrooker.BrookerNumber == null || lastBrooker.BrookerNumber == "B175822076159395")
            {
                lastBrooker.BrookerNumber = "0";
            }
            string newBrookerNumber = lastBrooker == null
                ? "1"
                : (int.Parse(lastBrooker.BrookerNumber) + 1).ToString("D1");

            var entity = reqModel.Adapt<Brooker>();
            entity.BrookerNumber = newBrookerNumber;
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