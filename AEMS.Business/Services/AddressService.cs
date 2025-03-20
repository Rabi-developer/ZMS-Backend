using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace IMS.Business.Services;

public interface IAddressService : IBaseService<AddressReq, AddressRes, Address>
{

}
public class AddressService : BaseService<AddressReq, AddressRes, AddressRepository, Address>, IAddressService
{


    private readonly IAddressRepository _repository;
    private readonly IHttpContextAccessor _context;
    public AddressService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<AddressRepository>();
        _context = context;
    }


}