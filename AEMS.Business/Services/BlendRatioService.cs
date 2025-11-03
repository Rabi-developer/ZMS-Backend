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
    public interface IBlendRatioService : IBaseService<BlendRatioReq, BlendRatioRes, BlendRatio>
    {
    }

    public class BlendRatioService : BaseService<BlendRatioReq, BlendRatioRes, BlendRatioRepository, BlendRatio>, IBlendRatioService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBlendRatioRepository _repository;
        protected readonly IUnitOfWork UnitOfWork;

        public BlendRatioService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork)
        {
            _context = dbContext;
            UnitOfWork = unitOfWork;
            _repository = UnitOfWork.GetRepository<BlendRatioRepository>();
        }

        public override async Task<Response<Guid>> Add(BlendRatioReq reqModel)
        {
            try
            {
                var lastBlendRatio = await _context.BlendRatio
                    .OrderByDescending(x => x.Listid)
                    .FirstOrDefaultAsync();

                string newListId = lastBlendRatio == null
                    ? "00000001"
                    : (int.Parse(lastBlendRatio.Listid) + 1).ToString("D8");

                var entity = reqModel.Adapt<BlendRatio>();
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
                var entity = await _context.BlendRatio
                    .FirstOrDefaultAsync(d => d.Id == id);
                if (entity == null)
                {
                    return new Response<bool>
                    {
                        StatusMessage = "BlendRatio not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                _context.BlendRatio.Remove(entity);
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