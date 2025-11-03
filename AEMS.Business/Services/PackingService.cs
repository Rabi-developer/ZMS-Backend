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
    public interface IPackingService : IBaseService<PackingReq, PackingRes, Packing>
    {

    }
    public class PackingService : BaseService<PackingReq, PackingRes, PackingRepository, Packing>, IPackingService
    {
        private readonly ApplicationDbContext _context;

        // Constructor with dependency injection
        public PackingService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork)
        {
            _context = dbContext;
        }

        // Add a new Packing entity
        public override async Task<Response<Guid>> Add(PackingReq reqModel)
        {
            try
            {
                // Get the last Packing to generate a new Listid
                var lastPacking = await _context.Packings
                    .OrderByDescending(x => x.Listid)
                    .FirstOrDefaultAsync();

                string newListId = lastPacking == null
                    ? "00000001"
                    : (int.Parse(lastPacking.Listid) + 1).ToString("D8");

                // Map request DTO to entity using Mapster
                var entity = reqModel.Adapt<Packing>();
                entity.Listid = newListId;
                entity.Id = Guid.NewGuid();
                entity.Descriptions = reqModel.Descriptions;
                entity.SubDescription = reqModel.SubDescription;

                // Add entity to repository and save changes
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

        // Delete a Packing entity by ID
        public override async Task<Response<bool>> Delete(Guid id)
        {
            try
            {
                var entity = await _context.Packings
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<bool>
                    {
                        StatusMessage = "Packing not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                _context.Packings.Remove(entity);
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

        // Example: Update a Packing entity (optional, added for completeness)
        public async Task<Response<Guid>> Update(Guid id, PackingReq reqModel)
        {
            try
            {
                var entity = await _context.Packings
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<Guid>
                    {
                        StatusMessage = "Packing not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                // Update entity fields
                entity.Descriptions = reqModel.Descriptions;
                entity.SubDescription = reqModel.SubDescription;

                _context.Packings.Update(entity);
                await UnitOfWork.SaveAsync();

                return new Response<Guid>
                {
                    Data = entity.Id,
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

        // Example: Get a Packing entity by ID (optional, added for completeness)
        public async Task<Response<PackingRes>> GetById(Guid id)
        {
            try
            {
                var entity = await _context.Packings
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<PackingRes>
                    {
                        StatusMessage = "Packing not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var response = entity.Adapt<PackingRes>();

                return new Response<PackingRes>
                {
                    Data = response,
                    StatusMessage = "Retrieved successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response<PackingRes>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}