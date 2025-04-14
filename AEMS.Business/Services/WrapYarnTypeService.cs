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
    public interface IWrapYarnTypeService : IBaseService<WrapYarnTypeReq, WrapYarnTypeRes, WrapYarnType>
    {
    }

    public class WrapYarnTypeService : BaseService<WrapYarnTypeReq, WrapYarnTypeRes, WrapYarnTypeRepository, WrapYarnType>, IWrapYarnTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWrapYarnTypeRepository _repository;
        protected readonly IUnitOfWork UnitOfWork;

        public WrapYarnTypeService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork)
        {
            _context = dbContext;
            UnitOfWork = unitOfWork;
            _repository = UnitOfWork.GetRepository<WrapYarnTypeRepository>();
        }

        public override async Task<Response<Guid>> Add(WrapYarnTypeReq reqModel)
        {
            try
            {
                var lastWrapYarnType = await _context.WrapYarnTypes
                    .OrderByDescending(x => x.Listid)
                    .FirstOrDefaultAsync();

                string newListId = lastWrapYarnType == null
                    ? "00000001"
                    : (int.Parse(lastWrapYarnType.Listid) + 1).ToString("D8");

                var entity = reqModel.Adapt<WrapYarnType>();
                entity.Listid = newListId;
                entity.Id = Guid.NewGuid();
                entity.Descriptions = reqModel.Descriptions;
                entity.SubDescription = reqModel.SubDescription;

                await Repository.Add(entity);
                await UnitOfWork.SaveAsync();

                return new Response<Guid>
                {
                    Data = entity.Id.Value,
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
                var entity = await _context.WrapYarnTypes
                    .FirstOrDefaultAsync(d => d.Id == id);
                if (entity == null)
                {
                    return new Response<bool>
                    {
                        StatusMessage = "WrapYarnType not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                _context.WrapYarnTypes.Remove(entity);
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