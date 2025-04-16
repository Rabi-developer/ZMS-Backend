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
    public interface IFabricTypeService : IBaseService<FabricTypeReq, FabricTypeRes, FabricType>
    {

    }
    public class FabricTypeService : BaseService<FabricTypeReq, FabricTypeRes, FabricTypeRepository, FabricType>, IFabricTypeService
    {
        private readonly ApplicationDbContext _context;

        // Constructor with dependency injection
        public FabricTypeService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork)
        {
            _context = dbContext;
        }

        // Add a new FabricType entity
        public override async Task<Response<Guid>> Add(FabricTypeReq reqModel)
        {
            try
            {
                // Get the last FabricType to generate a new Listid
                var lastFabricType = await _context.FabricTypes
                    .OrderByDescending(x => x.Listid)
                    .FirstOrDefaultAsync();

                string newListId = lastFabricType == null
                    ? "00000001"
                    : (int.Parse(lastFabricType.Listid) + 1).ToString("D8");

                // Map request DTO to entity using Mapster
                var entity = reqModel.Adapt<FabricType>();
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

        // Delete a FabricType entity by ID
        public override async Task<Response<bool>> Delete(Guid id)
        {
            try
            {
                var entity = await _context.FabricTypes
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<bool>
                    {
                        StatusMessage = "FabricType not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                _context.FabricTypes.Remove(entity);
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

        // Example: Update a FabricType entity (optional, added for completeness)
        public async Task<Response<Guid>> Update(Guid id, FabricTypeReq reqModel)
        {
            try
            {
                var entity = await _context.FabricTypes
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<Guid>
                    {
                        StatusMessage = "FabricType not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                // Update entity fields
                entity.Descriptions = reqModel.Descriptions;
                entity.SubDescription = reqModel.SubDescription;

                _context.FabricTypes.Update(entity);
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

        // Example: Get a FabricType entity by ID (optional, added for completeness)
        public async Task<Response<FabricTypeRes>> GetById(Guid id)
        {
            try
            {
                var entity = await _context.FabricTypes
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<FabricTypeRes>
                    {
                        StatusMessage = "FabricType not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var response = entity.Adapt<FabricTypeRes>();

                return new Response<FabricTypeRes>
                {
                    Data = response,
                    StatusMessage = "Retrieved successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response<FabricTypeRes>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}