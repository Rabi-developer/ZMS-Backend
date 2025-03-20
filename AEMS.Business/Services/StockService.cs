using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace IMS.Business.Services;

public interface IStockService : IBaseService<StockReq, StockRes, Stock>
{
}

public class StockService : BaseService<StockReq, StockRes, StockRepository, Stock>, IStockService
{
    private readonly IStockRepository _repository;
    private readonly IHttpContextAccessor _context;

    public StockService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<StockRepository>();
        _context = context;
    }
}
