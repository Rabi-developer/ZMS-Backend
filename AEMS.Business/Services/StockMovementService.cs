using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace IMS.Business.Services;

public interface IStockMovementService : IBaseService<StockMovementReq, StockMovementRes, StockMovement>
{

}
public class StockMovementService : BaseService<StockMovementReq, StockMovementRes, StockMovementRepository, StockMovement>, IStockMovementService
{


    private readonly IStockMovementRepository _repository;
    private readonly IHttpContextAccessor _context;
    public StockMovementService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<StockMovementRepository>();
        _context = context;
    }


}