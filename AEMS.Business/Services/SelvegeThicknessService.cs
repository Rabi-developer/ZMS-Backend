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
    public interface ISelvegeThicknessService : IBaseService<SelvegeThicknessReq, SelvegeThicknessRes, SelvegeThickness>
    {
    }

    public class SelvegeThicknessService : BaseService<SelvegeThicknessReq, SelvegeThicknessRes, SelvegeThicknessRepository, SelvegeThickness>, ISelvegeThicknessService
    {
        private readonly ApplicationDbContext _context;
        private readonly ISelvegeThicknessRepository _repository;
        protected readonly IUnitOfWork UnitOfWork;

        public SelvegeThicknessService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork)
        {
            _context = dbContext;
            UnitOfWork = unitOfWork;
            _repository = UnitOfWork.GetRepository<SelvegeThicknessRepository>();
        }

        public override async Task<Response<Guid>> Add(SelvegeThicknessReq reqModel)
        {
            try
            {
                var lastSelvegeThickness = await _context.SelvegeThicknesses
                    .OrderByDescending(x => x.Listid)
                    .FirstOrDefaultAsync();

                string newListId = lastSelvegeThickness == null
                    ? "00000001"
                    : (int.Parse(lastSelvegeThickness.Listid) + 1).ToString("D8");

                var entity = reqModel.Adapt<SelvegeThickness>();
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
                var entity = await _context.SelvegeThicknesses
                    .FirstOrDefaultAsync(d => d.Id == id);
                if (entity == null)
                {
                    return new Response<bool>
                    {
                        StatusMessage = "SelvegeThickness not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                _context.SelvegeThicknesses.Remove(entity);
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