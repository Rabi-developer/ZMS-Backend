using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;
using Mapster;
using System.Net;

namespace IMS.Business.Services;

public interface IDepartmentService : IBaseService<DepartmentReq, DepartmentRes, Department>
{

}
public class DepartmentService : BaseService<DepartmentReq, DepartmentRes, DepartmentRepository, Department>, IDepartmentService
{

    public DepartmentService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Response<DepartmentRes>> Get(Guid id)
    {
        try
        {
            var entity = await Repository.Get(id);
            return new Response<DepartmentRes>
            {
                Data = entity.Adapt<DepartmentRes>(),
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<DepartmentRes>
            {
                StatusMessage = e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}