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
using ZMS.Business.DTOs.Responses;
using ZMS.Domain.Entities;

namespace IMS.Business.Services;

public interface ISalesTaxService : IBaseService<SalesTaxReq, SalesTaxRes, SalesTax>
{
    Task<Response<IList<SalesTaxRes>>> GetAll(Pagination? paginate);

}

public class SalesTaxService : BaseService<SalesTaxReq, SalesTaxRes, SalesTaxRepository, SalesTax>, ISalesTaxService
{
    private readonly ISalesTaxRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public SalesTaxService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = (ISalesTaxRepository?)UnitOfWork.GetRepository<SalesTaxRepository>();
        _context = context;
        _DbContext = dbContextn;
    }



    public override async Task<Response<Guid>> Add(SalesTaxReq reqModel)
    {
        try
        {
            var lastSalesTax = await _DbContext.SalesTax
                .OrderByDescending(x => x.SalesTaxNumber)
                .FirstOrDefaultAsync();

            string newSalesTaxNumber = lastSalesTax == null
                ? "1"
                : (int.Parse(lastSalesTax.SalesTaxNumber) + 1).ToString("D1");

            var entity = reqModel.Adapt<SalesTax>();
            entity.SalesTaxNumber = newSalesTaxNumber;
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