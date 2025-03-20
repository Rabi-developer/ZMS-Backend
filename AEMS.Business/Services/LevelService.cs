using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;

namespace IMS.Business.Services;

public interface ILevelService : IBaseService<LevelReq, LevelRes, Level>
{

}
public class LevelService : BaseService<LevelReq, LevelRes, LevelRepository, Level>, ILevelService
{

    public LevelService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

}