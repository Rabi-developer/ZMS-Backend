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
    public interface ISelvegeWidthService : IBaseService<SelvegeWidthReq, SelvegeWidthRes, SelvegeWidth>
    {

    }
    public class SelvegeWidthService : BaseService<SelvegeWidthReq, SelvegeWidthRes, SelvegeWidthRepository, SelvegeWidth>, ISelvegeWidthService
    {
        private readonly ApplicationDbContext _context;

        // Constructor with dependency injection
        public SelvegeWidthService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork)
        {
            _context = dbContext;
        }

        // Add a new SelvegeWidth entity
        public override async Task<Response<Guid>> Add(SelvegeWidthReq reqModel)
        {
            try
            {
                // Get the last SelvegeWidth to generate a new Listid
                var lastSelvegeWidth = await _context.SelvegeWidths
                    .OrderByDescending(x => x.Listid)
                    .FirstOrDefaultAsync();

                string newListId = lastSelvegeWidth == null
                    ? "00000001"
                    : (int.Parse(lastSelvegeWidth.Listid) + 1).ToString("D8");

                // Map request DTO to entity using Mapster
                var entity = reqModel.Adapt<SelvegeWidth>();
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

        // Delete a SelvegeWidth entity by ID
        public override async Task<Response<bool>> Delete(Guid id)
        {
            try
            {
                var entity = await _context.SelvegeWidths
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<bool>
                    {
                        StatusMessage = "SelvegeWidth not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                _context.SelvegeWidths.Remove(entity);
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

        // Example: Update a SelvegeWidth entity (optional, added for completeness)
        public async Task<Response<Guid>> Update(Guid id, SelvegeWidthReq reqModel)
        {
            try
            {
                var entity = await _context.SelvegeWidths
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<Guid>
                    {
                        StatusMessage = "SelvegeWidth not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                // Update entity fields
                entity.Descriptions = reqModel.Descriptions;
                entity.SubDescription = reqModel.SubDescription;

                _context.SelvegeWidths.Update(entity);
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

        // Example: Get a SelvegeWidth entity by ID (optional, added for completeness)
        public async Task<Response<SelvegeWidthRes>> GetById(Guid id)
        {
            try
            {
                var entity = await _context.SelvegeWidths
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<SelvegeWidthRes>
                    {
                        StatusMessage = "SelvegeWidth not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var response = entity.Adapt<SelvegeWidthRes>();

                return new Response<SelvegeWidthRes>
                {
                    Data = response,
                    StatusMessage = "Retrieved successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response<SelvegeWidthRes>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}