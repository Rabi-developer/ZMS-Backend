using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace IMS.Business.Services;

public interface IPriceService : IBaseService<PriceReq, PriceRes, Price>
{

}
public class PriceService : BaseService<PriceReq, PriceRes, PriceRepository, Price>, IPriceService
{


    private readonly IPriceRepository _repository;
    private readonly IHttpContextAccessor _context;
    public PriceService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<PriceRepository>();
        _context = context;
    }


}