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
    public interface IPickInsertionService : IBaseService<PickInsertionReq, PickInsertionRes, PickInsertion>
    {
    }

    public class PickInsertionService : BaseService<PickInsertionReq, PickInsertionRes, PickInsertionRepository, PickInsertion>, IPickInsertionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPickInsertionRepository _repository;
        protected readonly IUnitOfWork UnitOfWork;

        public PickInsertionService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork)
        {
            _context = dbContext;
            UnitOfWork = unitOfWork;
            _repository = UnitOfWork.GetRepository<PickInsertionRepository>();
        }

        public override async Task<Response<Guid>> Add(PickInsertionReq reqModel)
        {
            try
            {
                var lastPickInsertion = await _context.PickInsertion
                    .OrderByDescending(x => x.Listid)
                    .FirstOrDefaultAsync();

                string newListId = lastPickInsertion == null
                    ? "00000001"
                    : (int.Parse(lastPickInsertion.Listid) + 1).ToString("D8");

                var entity = reqModel.Adapt<PickInsertion>();
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
                var entity = await _context.PickInsertion
                    .FirstOrDefaultAsync(d => d.Id == id);
                if (entity == null)
                {
                    return new Response<bool>
                    {
                        StatusMessage = "PickInsertion not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                _context.PickInsertion.Remove(entity);
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