using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Context;
using IMS.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace IMS.Business.Services
{
    public interface IWeftYarnTypeService : IBaseService<WeftYarnTypeReq, WeftYarnTypeRes, WeftYarnType>
    {
    }

    public class WeftYarnTypeService : BaseService<WeftYarnTypeReq, WeftYarnTypeRes, WeftYarnTypeRepository, WeftYarnType>, IWeftYarnTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWeftYarnTypeRepository _repository;
        protected readonly IUnitOfWork UnitOfWork;

        public WeftYarnTypeService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork)
        {
            _context = dbContext;
            UnitOfWork = unitOfWork;
            _repository = UnitOfWork.GetRepository<WeftYarnTypeRepository>();
        }

        public override async Task<Response<Guid>> Add(WeftYarnTypeReq reqModel)
        {
            try
            {
                var lastWeftYarnType = await _context.WeftYarnTypes
                    .OrderByDescending(x => x.Listid)
                    .FirstOrDefaultAsync();

                string newListId = lastWeftYarnType == null
                    ? "00000001"
                    : (int.Parse(lastWeftYarnType.Listid) + 1).ToString("D8");

                var entity = reqModel.Adapt<WeftYarnType>();
                entity.Listid = newListId;
                entity.Id = Guid.NewGuid();
                entity.Descriptions = reqModel.Descriptions;
                entity.SubDescription = reqModel.SubDescription;

                await Repository.Add(entity);
                await UnitOfWork.SaveAsync();

                return new Response<Guid>
                {
                    Data = entity.Id,
                    StatusMessage = "Created successfully",
                    StatusCode = HttpStatusCode.Created
                };
            }
            catch (Exception e)
            {
                return new Response<Guid>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
        public override async Task<Response<bool>> Delete(Guid id)
        {
            try
            {
                var entity = await _context.WeftYarnTypes
                    .FirstOrDefaultAsync(d => d.Id == id);
                if (entity == null)
                {
                    return new Response<bool>
                    {
                        StatusMessage = "WeftYarnType not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                _context.WeftYarnTypes.Remove(entity);
                await UnitOfWork.SaveAsync();

                return new Response<bool>
                {
                    Data = true,
                    StatusMessage = "Deleted successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response<bool>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}