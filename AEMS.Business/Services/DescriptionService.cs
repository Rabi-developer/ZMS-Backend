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
    public interface IDescriptionService : IBaseService<DescriptionReq, DescriptionRes, Description>
    {
    }

    public class DescriptionService : BaseService<DescriptionReq, DescriptionRes, DescriptionRepository, Description>, IDescriptionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IDescriptionRepository _repository;
        protected readonly IUnitOfWork UnitOfWork;

        public DescriptionService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork)
        {
            _context = dbContext;
            UnitOfWork = unitOfWork;
            _repository = UnitOfWork.GetRepository<DescriptionRepository>();
        }

        public override async Task<Response<Guid>> Add(DescriptionReq reqModel)
        {
            try
            {
                var lastDescription = await _context.Descriptions
                    .OrderByDescending(x => x.Listid)
                    .FirstOrDefaultAsync();

                string newListId = lastDescription == null
                    ? "00000001"
                    : (int.Parse(lastDescription.Listid) + 1).ToString("D8");

                var entity = reqModel.Adapt<Description>();
                entity.Listid = newListId;
                entity.Id = Guid.NewGuid();
                entity.Descriptions = reqModel.Descriptions; 

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
                var entity = await _context.Descriptions
                    .FirstOrDefaultAsync(d => d.Id == id);
                if (entity == null)
                {
                    return new Response<bool>
                    {
                        StatusMessage = "Description not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                _context.Descriptions.Remove(entity);
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