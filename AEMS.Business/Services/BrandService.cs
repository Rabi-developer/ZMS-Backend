using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace IMS.Business.Services;

public interface IBrandService : IBaseService<BrandReq, BrandRes, Brand>
{

}
public class BrandService : BaseService<BrandReq, BrandRes, BrandRepository, Brand>, IBrandService
{


    private readonly IBrandRepository _repository;
    private readonly IHttpContextAccessor _context;
    public BrandService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<BrandRepository>();
        _context = context;
    }


}