using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Base;
using IMS.Domain.Context;
using IMS.Domain.Entities;
using IMS.Domain.Utilities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace IMS.Business.Services
{
    public interface ISelvegeWeavesService : IBaseService<SelvegeWeavesReq, SelvegeWeavesRes, SelvegeWeaves>
    {

    }
    public class SelvegeWeavesService : BaseService<SelvegeWeavesReq, SelvegeWeavesRes, SelvegeWeavesRepository, SelvegeWeaves>, ISelvegeWeavesService
    {
        private readonly ApplicationDbContext _context;

        // Constructor with dependency injection
        public SelvegeWeavesService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork)
        {
            _context = dbContext;
        }

        // Add a new SelvegeWeaves entity
        public override async Task<Response<Guid>> Add(SelvegeWeavesReq reqModel)
        {
            try
            {
                // Get the last SelvegeWeaves to generate a new Listid
                var lastSelvegeWeaves = await _context.SelvegeWeaves
                    .OrderByDescending(x => x.Listid)
                    .FirstOrDefaultAsync();

                string newListId = lastSelvegeWeaves == null
                    ? "00000001"
                    : (int.Parse(lastSelvegeWeaves.Listid) + 1).ToString("D8");

                // Map request DTO to entity using Mapster
                var entity = reqModel.Adapt<SelvegeWeaves>();
                entity.Listid = newListId;
                entity.Id = Guid.NewGuid();
                entity.Descriptions = reqModel.Descriptions;
                entity.SubDescription = reqModel.SubDescription;

                // Add entity to repository and save changes
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

        // Delete a SelvegeWeaves entity by ID
        public override async Task<Response<bool>> Delete(Guid id)
        {
            try
            {
                var entity = await _context.SelvegeWeaves
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<bool>
                    {
                        StatusMessage = "SelvegeWeaves not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                _context.SelvegeWeaves.Remove(entity);
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

        // Example: Update a SelvegeWeaves entity (optional, added for completeness)
        public async Task<Response<Guid>> Update(Guid id, SelvegeWeavesReq reqModel)
        {
            try
            {
                var entity = await _context.SelvegeWeaves
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<Guid>
                    {
                        StatusMessage = "SelvegeWeaves not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                // Update entity fields
                entity.Descriptions = reqModel.Descriptions;
                entity.SubDescription = reqModel.SubDescription;

                _context.SelvegeWeaves.Update(entity);
                await UnitOfWork.SaveAsync();

                return new Response<Guid>
                {
                    Data = entity.Id.Value,
                    StatusMessage = "Updated successfully",
                    StatusCode = HttpStatusCode.OK
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

        // Example: Get a SelvegeWeaves entity by ID (optional, added for completeness)
        public async Task<Response<SelvegeWeavesRes>> GetById(Guid id)
        {
            try
            {
                var entity = await _context.SelvegeWeaves
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<SelvegeWeavesRes>
                    {
                        StatusMessage = "SelvegeWeaves not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var response = entity.Adapt<SelvegeWeavesRes>();

                return new Response<SelvegeWeavesRes>
                {
                    Data = response,
                    StatusMessage = "Retrieved successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response<SelvegeWeavesRes>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}