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
    public interface IEndUseService : IBaseService<EndUseReq, EndUseRes, EndUse>
    {

    }
    public class EndUseService : BaseService<EndUseReq, EndUseRes, EndUseRepository, EndUse>, IEndUseService
    {
        private readonly ApplicationDbContext _context;

        // Constructor with dependency injection
        public EndUseService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork)
        {
            _context = dbContext;
        }

        // Add a new EndUse entity
        public override async Task<Response<Guid>> Add(EndUseReq reqModel)
        {
            try
            {
                // Get the last EndUse to generate a new Listid
                var lastEndUse = await _context.EndUses
                    .OrderByDescending(x => x.Listid)
                    .FirstOrDefaultAsync();

                string newListId = lastEndUse == null
                    ? "00000001"
                    : (int.Parse(lastEndUse.Listid) + 1).ToString("D8");

                // Map request DTO to entity using Mapster
                var entity = reqModel.Adapt<EndUse>();
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

        // Delete a EndUse entity by ID
        public override async Task<Response<bool>> Delete(Guid id)
        {
            try
            {
                var entity = await _context.EndUses
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<bool>
                    {
                        StatusMessage = "EndUse not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                _context.EndUses.Remove(entity);
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

        // Example: Update a EndUse entity (optional, added for completeness)
        public async Task<Response<Guid>> Update(Guid id, EndUseReq reqModel)
        {
            try
            {
                var entity = await _context.EndUses
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<Guid>
                    {
                        StatusMessage = "EndUse not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                // Update entity fields
                entity.Descriptions = reqModel.Descriptions;
                entity.SubDescription = reqModel.SubDescription;

                _context.EndUses.Update(entity);
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

        // Example: Get a EndUse entity by ID (optional, added for completeness)
        public async Task<Response<EndUseRes>> GetById(Guid id)
        {
            try
            {
                var entity = await _context.EndUses
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<EndUseRes>
                    {
                        StatusMessage = "EndUse not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var response = entity.Adapt<EndUseRes>();

                return new Response<EndUseRes>
                {
                    Data = response,
                    StatusMessage = "Retrieved successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response<EndUseRes>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}