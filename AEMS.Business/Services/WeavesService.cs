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
    public interface IWeavesService : IBaseService<WeavesReq, WeavesRes, Weaves>
    {
    }

    public class WeavesService : BaseService<WeavesReq, WeavesRes, WeavesRepository, Weaves>, IWeavesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWeavesRepository _repository;
        protected readonly IUnitOfWork UnitOfWork;

        public WeavesService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork)
        {
            _context = dbContext;
            UnitOfWork = unitOfWork;
            _repository = UnitOfWork.GetRepository<WeavesRepository>();
        }

        public override async Task<Response<Guid>> Add(WeavesReq reqModel)
        {
            try
            {
                var lastWeaves = await _context.Weaves
                    .OrderByDescending(x => x.Listid)
                    .FirstOrDefaultAsync();

                string newListId = lastWeaves == null
                    ? "00000001"
                    : (int.Parse(lastWeaves.Listid) + 1).ToString("D8");

                var entity = reqModel.Adapt<Weaves>();
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
                var entity = await _context.Weaves
                    .FirstOrDefaultAsync(d => d.Id == id);
                if (entity == null)
                {
                    return new Response<bool>
                    {
                        StatusMessage = "Weaves not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                _context.Weaves.Remove(entity);
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