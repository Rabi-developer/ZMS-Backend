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

public interface IBusinessAssociateService : IBaseService<BusinessAssociateReq, BusinessAssociateRes, BusinessAssociate>
{
    Task<Response<IList<BusinessAssociateRes>>> GetAll(Pagination? paginate);

}

public class BusinessAssociateService : BaseService<BusinessAssociateReq, BusinessAssociateRes, BusinessAssociateRepository, BusinessAssociate>, IBusinessAssociateService
{
    private readonly IBusinessAssociateRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public BusinessAssociateService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = (IBusinessAssociateRepository?)UnitOfWork.GetRepository<BusinessAssociateRepository>();
        _context = context;
        _DbContext = dbContextn;
    }



    public override async Task<Response<Guid>> Add(BusinessAssociateReq reqModel)
    {
        try
        {
            var lastBusinessAssociate = await _DbContext.BusinessAssociate
                .OrderByDescending(x => x.BusinessAssociateNumber)
                .FirstOrDefaultAsync();
            if (lastBusinessAssociate.BusinessAssociateNumber == null || lastBusinessAssociate.BusinessAssociateNumber == "BA1758222418874226")
            {
                lastBusinessAssociate.BusinessAssociateNumber = "0";
            }

            string newBusinessAssociateNumber = lastBusinessAssociate == null
                ? "1"
                : (int.Parse(lastBusinessAssociate.BusinessAssociateNumber) + 1).ToString("D1");

            var entity = reqModel.Adapt<BusinessAssociate>();
            entity.BusinessAssociateNumber = newBusinessAssociateNumber;
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