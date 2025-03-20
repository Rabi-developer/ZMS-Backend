using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;
using Mapster;
using System.Net;

namespace IMS.Business.Services
{
    public interface IEmployeeService : IBaseService<EmployeeReq, EmployeeRes, Employee>
    {

    }
    public class EmployeeService : BaseService<EmployeeReq, EmployeeRes, EmployeeRepository, Employee>, IEmployeeService
    {
        public EmployeeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }


    }

}