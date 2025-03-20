using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace IMS.Business.Services;

public interface IStockReorderService : IBaseService<StockReorderReq, StockReorderRes, StockReorder>
{

}
public class StockReorderService : BaseService<StockReorderReq, StockReorderRes, StockReorderRepository, StockReorder>, IStockReorderService
{


    private readonly IStockReorderRepository _repository;
    private readonly IHttpContextAccessor _context;
    public StockReorderService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<StockReorderRepository>();
        _context = context;
    }


}