using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;
using IMS.Domain.Utilities;
using Mapster;
using System.Net;

namespace IMS.Business.Services;

public interface IOrganizationService : IBaseMinService
{
    Task<Response<object>> Add(OrganizationReq reqModel, string userId);
    Task<Response<IList<OrganizationRes>>> GetAll(Pagination pagination, bool onlyusers);
    Task<Response<OrganizationRes>> Get(Guid id);
    Task<Response<OrganizationRes>> Update(OrganizationUpdateReq reqModel);
    Task<Response<bool>> Delete(Guid id);
}
public class OrganizationService : BaseService<SignUpReq, object, OrganizationRepository, Organization>, IOrganizationService
{

    public OrganizationService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }


    public async Task<Response<object>> Add(OrganizationReq reqModel, string userId)
    {
        var trans = await UnitOfWork.BeginTransactionAsync();
        try
        {
            Organization org = new Organization
            {
                Name = reqModel.Name,
                Description = reqModel.Description,
                AddressLine1 = reqModel.AddressLine1,
                AddressLine2 = reqModel.AddressLine2,
                City = reqModel.City,
                State = reqModel.State,
                Country = reqModel.Country,
                Zip = reqModel.Zip,
                CreatedDateTime = DateTime.UtcNow,
                Email = reqModel.Email,
                Website = reqModel.Website
            };
            await Repository.Add(org);
            await UnitOfWork.SaveAsync();

            OrganizationUser organizationUser = new OrganizationUser
            {
                OrganizationId = org.Id,
                UserId = Guid.Parse(userId)
            };
            await UnitOfWork._context.OrganizationUsers.AddAsync(organizationUser);
            await UnitOfWork.SaveAsync();

            await UnitOfWork.CommitTransactionAsync(trans);
            return new Response<object>
            {
                Data = org.Id,
                StatusCode = HttpStatusCode.Created,
                StatusMessage = "Created successfully"
            };
        }
        catch (Exception e)
        {
            await UnitOfWork.RollBackTransactionAsync(trans);
            return new Response<object>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                StatusMessage = e.Message
            };
        }
    }

    public async Task<Response<IList<OrganizationRes>>> GetAll(Pagination pagination, bool onlyusers = true)
    {
        try
        {
            var (pag, data) = await Repository.GetAll(pagination, onlyusers);

            var res = data.Adapt<List<OrganizationRes>>();

            return new Response<IList<OrganizationRes>>
            {
                Data = res,
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<OrganizationRes>>
            {
                StatusMessage = e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<Response<OrganizationRes>> Get(Guid id)
    {
        try
        {
            var entity = await Repository.Get(id, null);
            var res = entity.Adapt<OrganizationRes>();
            return new Response<OrganizationRes>
            {
                Data = res,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<OrganizationRes>
            {
                StatusMessage = e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<Response<OrganizationRes>> Update(OrganizationUpdateReq reqModel)
    {
        var trans = await UnitOfWork.BeginTransactionAsync();
        try
        {
            var result = await Repository.Update(reqModel.Adapt<Organization>(), null);
            await UnitOfWork.SaveAsync();

            await UnitOfWork.CommitTransactionAsync(trans);
            var res = result.Adapt<OrganizationRes>();
            return new Response<OrganizationRes>
            {
                Data = res,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            await UnitOfWork.RollBackTransactionAsync(trans);
            return new Response<OrganizationRes>
            {
                StatusMessage = e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

}