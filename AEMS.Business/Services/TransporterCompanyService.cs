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
    public interface ITransporterCompanyService : IBaseService<TransporterCompanyReq, TransporterCompanyRes, TransporterCompany>
    {

    }
    public class TransporterCompanyService : BaseService<TransporterCompanyReq, TransporterCompanyRes, TransporterCompanyRepository, TransporterCompany>, ITransporterCompanyService
    {
        private readonly ApplicationDbContext _context;

        // Constructor with dependency injection
        public TransporterCompanyService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork)
        {
            _context = dbContext;
        }

        // Add a new TransporterCompany entity
        public override async Task<Response<Guid>> Add(TransporterCompanyReq reqModel)
        {
            try
            {
                // Get the last TransporterCompany to generate a new Listid
                var lastTransporterCompany = await _context.TransporterCompanies
                    .OrderByDescending(x => x.Listid)
                    .FirstOrDefaultAsync();

                string newListId = lastTransporterCompany == null
                    ? "001"
                    : (int.Parse(lastTransporterCompany.Listid) + 1).ToString("D3");

                // Map request DTO to entity using Mapster
                var entity = reqModel.Adapt<TransporterCompany>();
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

        // Delete a TransporterCompany entity by ID
        public override async Task<Response<bool>> Delete(Guid id)
        {
            try
            {
                var entity = await _context.TransporterCompanies
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<bool>
                    {
                        StatusMessage = "TransporterCompany not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                _context.TransporterCompanies.Remove(entity);
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

        // Example: Update a TransporterCompany entity (optional, added for completeness)
        public async Task<Response<Guid>> Update(Guid id, TransporterCompanyReq reqModel)
        {
            try
            {
                var entity = await _context.TransporterCompanies
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<Guid>
                    {
                        StatusMessage = "TransporterCompany not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                // Update entity fields
                entity.Descriptions = reqModel.Descriptions;
                entity.Segment = reqModel.Segment;

                _context.TransporterCompanies.Update(entity);
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

        // Example: Get a TransporterCompany entity by ID (optional, added for completeness)
        public async Task<Response<TransporterCompanyRes>> GetById(Guid id)
        {
            try
            {
                var entity = await _context.TransporterCompanies
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (entity == null)
                {
                    return new Response<TransporterCompanyRes>
                    {
                        StatusMessage = "TransporterCompany not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var response = entity.Adapt<TransporterCompanyRes>();

                return new Response<TransporterCompanyRes>
                {
                    Data = response,
                    StatusMessage = "Retrieved successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response<TransporterCompanyRes>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}