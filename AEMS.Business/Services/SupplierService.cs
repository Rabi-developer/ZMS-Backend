using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;
using Mapster;

namespace IMS.Business.Services
{
    public interface ISupplierService : IBaseService<SupplierReq, SupplierRes, Supplier>
    {

    }
    public class SupplierService : BaseService<SupplierReq, SupplierRes, SupplierRepository, Supplier>, ISupplierService
    {
        public SupplierService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}