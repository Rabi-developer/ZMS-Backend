using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace IMS.Business.Services;

public interface IUnitService : IBaseService<UnitReq, UnitRes, Unit>
{

}
public class UnitService : BaseService<UnitReq, UnitRes, UnitRepository, Unit>, IUnitService
{


    private readonly IUnitRepository _repository;
    private readonly IHttpContextAccessor _context;
    public UnitService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<UnitRepository>();
        _context = context;
    }


}