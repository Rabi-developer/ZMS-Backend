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
using ZMS.Business.DTOs.Requests;
using ZMS.Business.DTOs.Responses;
using ZMS.DataAccess.Repositories;
using ZMS.Domain.Entities;

namespace IMS.Business.Services
{
    public interface IDeliveryTermService : IBaseService<DeliveryTermReq, DeliveryTermRes, DeliveryTerm>
    {

    }
    public class DeliveryTermService : BaseService<DeliveryTermReq, DeliveryTermRes, DeliveryTermRepository, DeliveryTerm>, IDeliveryTermService
    {
        private readonly ApplicationDbContext _context;

        // Constructor with dependency injection
        public DeliveryTermService(IUnitOfWork unitOfWork, ApplicationDbContext dbContextN ): base(unitOfWork)
        {
            _context = dbContextN;
        }

        // Add a new DeliveryTerm entity
        public override async Task<Response<Guid>> Add(DeliveryTermReq reqModel)
        {
            try
            {
                // Get the last DeliveryTerm to generate a new Listid
                var lastDeliveryTerm = await _context.DeliveryTerms
                    .OrderByDescending(x => x.Listid)
                    .FirstOrDefaultAsync();

                string newListId = lastDeliveryTerm == null
                    ? "001"
                    : (int.Parse(lastDeliveryTerm.Listid) + 1).ToString("D3");

                // Map request DTO to entity using Mapster
                var entity = reqModel.Adapt<DeliveryTerm>();
                entity.Listid = newListId;
                entity.Id = Guid.NewGuid();
                entity.Descriptions = reqModel.Descriptions;
                entity.Segment = reqModel.Segment;

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

        // Delete a DeliveryTerm entity by ID
        public override async Task<Response<bool>> Delete(Guid id)
        {
            try
            {
                var entity = await _context.DeliveryTerms
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<bool>
                    {
                        StatusMessage = "DeliveryTerm not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                _context.DeliveryTerms.Remove(entity);
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

        // Example: Update a DeliveryTerm entity (optional, added for completeness)
        public async Task<Response<Guid>> Update(Guid id, DeliveryTermReq reqModel)
        {
            try
            {
                var entity = await _context.DeliveryTerms
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<Guid>
                    {
                        StatusMessage = "DeliveryTerm not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                // Update entity fields
                entity.Descriptions = reqModel.Descriptions;
                entity.Segment = reqModel.Segment;

                _context.DeliveryTerms.Update(entity);
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

        // Example: Get a DeliveryTerm entity by ID (optional, added for completeness)
        public async Task<Response<DeliveryTermRes>> GetById(Guid id)
        {
            try
            {
                var entity = await _context.DeliveryTerms
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<DeliveryTermRes>
                    {
                        StatusMessage = "DeliveryTerm not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var response = entity.Adapt<DeliveryTermRes>();

                return new Response<DeliveryTermRes>
                {
                    Data = response,
                    StatusMessage = "Retrieved successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response<DeliveryTermRes>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}