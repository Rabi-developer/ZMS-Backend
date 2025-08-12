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

public interface IVendorService : IBaseService<VendorReq, VendorRes, Vendor>
{
    Task<Response<IList<VendorRes>>> GetAll(Pagination? paginate);

}

public class VendorService : BaseService<VendorReq, VendorRes, VendorRepository, Vendor>, IVendorService
{
    private readonly IVendorRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public VendorService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = (IVendorRepository?)UnitOfWork.GetRepository<VendorRepository>();
        _context = context;
        _DbContext = dbContextn;
    }



    public override async Task<Response<Guid>> Add(VendorReq reqModel)
    {
        try
        {
            var lastVendor = await _DbContext.Vendor
                .OrderByDescending(x => x.VendorNumber)
                .FirstOrDefaultAsync();

            string newVendorNumber = lastVendor == null
                ? "1"
                : (int.Parse(lastVendor.VendorNumber) + 1).ToString("D1");

            var entity = reqModel.Adapt<Vendor>();
            entity.VendorNumber = newVendorNumber;
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