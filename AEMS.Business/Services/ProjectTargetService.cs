using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;
using Mapster;
using System.Net;

namespace IMS.Business.Services;

public interface IProjectTargetService : IBaseService<ProjectTargetReq, ProjectTargetRes, ProjectTarget>
{

}
public class ProjectTargetService : BaseService<ProjectTargetReq, ProjectTargetRes, ProjectTargetRepository, ProjectTarget>, IProjectTargetService
{

    public ProjectTargetService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

  
}