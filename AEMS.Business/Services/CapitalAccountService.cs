using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;
using Mapster;
using System.Net;
using static IMS.DataAccess.Repositories.ICapitalAccountRepository;

namespace IMS.Business.Services;

public interface ICapitalAccountService : IBaseService<CapitalAccountReq, CapitalAccountRes, CapitalAccount>
{
    Task<IList<CapitalAccount>> GetByParent (Guid ParentId);
}
public class CapitalAccountService : BaseService<CapitalAccountReq, CapitalAccountRes, CapitalAccountRepository, CapitalAccount>, ICapitalAccountService
{

    public CapitalAccountService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }


    public async Task<IList<CapitalAccount>> GetByParent(Guid ParentId)
    {
        return await Repository.GetByParent(ParentId);
    }
}