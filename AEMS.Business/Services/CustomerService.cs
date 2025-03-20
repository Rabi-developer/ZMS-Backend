using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;
using Mapster;

namespace IMS.Business.Services
{
    public interface ICustomerService : IBaseService<CustomerReq, CustomerRes, Customer>
    {

    }
    public class CustomerService : BaseService<CustomerReq, CustomerRes, CustomerRepository, Customer>, ICustomerService
    {
        public CustomerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}