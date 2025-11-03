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

public interface ITransporterService : IBaseService<TransporterReq, TransporterRes, Transporter>
{
    Task<Response<IList<TransporterRes>>> GetAll(Pagination? paginate);

}

public class TransporterService : BaseService<TransporterReq, TransporterRes, TransporterRepository, Transporter>, ITransporterService
{
    private readonly ITransporterRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public TransporterService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = (ITransporterRepository?)UnitOfWork.GetRepository<TransporterRepository>();
        _context = context;
        _DbContext = dbContextn;
    }



   /* public override async Task<Response<Guid>> Add(TransporterReq reqModel)
    {
        try
        { 
              var lastTransporter = await _DbContext.Transporters
              .OrderByDescending(x => x.TransporterNumber)
              .FirstOrDefaultAsync();
        if (lastTransporter.TransporterNumber == null || lastTransporter.TransporterNumber == "T1758281857701166")
        {
                lastTransporter.TransporterNumber = "0";
        }
        string newTransporterNumber = lastTransporter == null
            ? "1"
            : (int.Parse(lastTransporter.TransporterNumber) + 1).ToString("D1");
 
           

            var entity = reqModel.Adapt<Transporter>();
            entity.TransporterNumber = newTransporterNumber;
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
    }*/

}