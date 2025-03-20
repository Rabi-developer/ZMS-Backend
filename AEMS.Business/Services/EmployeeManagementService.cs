using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;
using Mapster;
using System.Net;

namespace IMS.Business.Services;

public interface IEmployeeManagementService : IBaseService<EmployeeManagementReq, EmployeeManagementRes, EmployeeManagement>
{

}
public class EmployeeManagementService : BaseService<EmployeeManagementReq, EmployeeManagementRes, EmployeeManagementRepository, EmployeeManagement>, IEmployeeManagementService
{

    public EmployeeManagementService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

   
}