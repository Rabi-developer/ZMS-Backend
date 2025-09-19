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
using ZMS.Business.DTOs.Requests;
using ZMS.Domain.Entities;

namespace IMS.Business.Services;

public interface IMunshyanaService : IBaseService<MunshyanaReq, MunshyanaRes, Munshyana>
{
    Task<Response<IList<MunshyanaRes>>> GetAll(Pagination? paginate);

}

public class MunshyanaService : BaseService<MunshyanaReq, MunshyanaRes, MunshyanaRepository, Munshyana>, IMunshyanaService
{
    private readonly IMunshyanaRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public MunshyanaService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = (IMunshyanaRepository?)UnitOfWork.GetRepository<MunshyanaRepository>();
        _context = context;
        _DbContext = dbContextn;
    }



    public override async Task<Response<Guid>> Add(MunshyanaReq reqModel)
    {
        try
        {
            var lastMunshyana = await _DbContext.Munshyana
                .OrderByDescending(x => x.MunshyanaNumber)
                .FirstOrDefaultAsync();

            if (lastMunshyana.MunshyanaNumber == null || lastMunshyana.MunshyanaNumber == "M1758222863648799")
            {
                lastMunshyana.MunshyanaNumber = "0";
            }
            string newMunshyanaNumber = lastMunshyana == null
                ? "1"
                : (int.Parse(lastMunshyana.MunshyanaNumber) + 1).ToString("D1");

            var entity = reqModel.Adapt<Munshyana>();
            entity.MunshyanaNumber = newMunshyanaNumber;
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