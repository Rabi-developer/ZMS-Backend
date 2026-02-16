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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ZMS.Business.DTOs.Requests;
using ZMS.Domain.Entities;

namespace IMS.Business.Services
{
    public interface IOpeningBalanceService : IBaseService<OpeningBalanceReq, OpeningBalanceRes, OpeningBalance>
    {

        Task<Response<IList<OpeningBalanceRes>>> GetAll(Pagination? paginate);
        Task<Response<OpeningBalanceRes>> Update(int openingNo, OpeningBalanceReq reqModel);
        // Optional: if you use status like EntryVoucher
        // Task<OpeningBalanceStatus> UpdateStatusAsync(int openingNo, string status);
    }

    public class OpeningBalanceService : BaseService<OpeningBalanceReq, OpeningBalanceRes, OpeningBalanceRepository, OpeningBalance>,
        IOpeningBalanceService
    {
        private readonly IOpeningBalanceRepository _repository;
        private readonly IHttpContextAccessor _context;
        private readonly ApplicationDbContext _dbContext;

        public OpeningBalanceService(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor context,
            ApplicationDbContext dbContext)
            : base(unitOfWork)
        {
            _repository = unitOfWork.GetRepository<OpeningBalanceRepository>();
            _context = context;
            _dbContext = dbContext;
        }

        public  async Task<Response<IList<OpeningBalanceRes>>> GetAll(Pagination? paginate)
        {
            try
            {
                var pagination = paginate ?? new Pagination();
                var (pag, data) = await Repository.GetAll(pagination, q => q.Include(x => x.OpeningBalanceEntrys));

                var result = data.Adapt<List<OpeningBalanceRes>>();

                return new Response<IList<OpeningBalanceRes>>
                {
                    Data = result,
                    Misc = pag,
                    StatusMessage = "Fetched successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new Response<IList<OpeningBalanceRes>>
                {
                    StatusMessage = ex.InnerException?.Message ?? ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public override async Task<Response<Guid>> Add(OpeningBalanceReq reqModel)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var entity = reqModel.Adapt<OpeningBalance>();
                entity.Id = Guid.NewGuid();

                // Auto-increment OpeningNo
             /*   var lastNo = await _dbContext.OpeningBalances
                    .MaxAsync(x => (int?)x.OpeningNo) ?? 0;
                entity.OpeningNo = lastNo + 1;*/

                entity.CreatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
                entity.CreationDate = DateTime.UtcNow.ToString("o");

                entity.OpeningBalanceEntrys = reqModel.OpeningBalanceEntrys?.Select(e => new OpeningBalanceEntry
                {
                    Id = Guid.NewGuid(),
                    BiltyNo = e.BiltyNo,
                    BiltyDate = e.BiltyDate,
                    VehicleNo = e.VehicleNo,
                    City = e.City,
                    Customer = e.Customer,
                    Broker = e.Broker,
                    ChargeType = e.ChargeType,
                    Debit = e.Debit,
                    Credit = e.Credit
                }).ToList() ?? new List<OpeningBalanceEntry>();

                await Repository.Add(entity);
                await UnitOfWork.SaveAsync();
                await transaction.CommitAsync();

                return new Response<Guid>
                {
                    Data = entity.Id,
                    StatusMessage = "Created successfully",
                    StatusCode = HttpStatusCode.Created
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new Response<Guid>
                {
                    StatusMessage = ex.InnerException?.Message ?? ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<OpeningBalanceRes>> Get(Guid id)
        {
            try
            {
                var entity = await Repository.Get(id, q => q.Include(x => x.OpeningBalanceEntrys));
                if (entity == null)
                {
                    return new Response<OpeningBalanceRes>
                    {
                        StatusMessage = "Opening balance not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                return new Response<OpeningBalanceRes>
                {
                    Data = entity.Adapt<OpeningBalanceRes>(),
                    StatusMessage = "Fetched successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new Response<OpeningBalanceRes>
                {
                    StatusMessage = ex.InnerException?.Message ?? ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<OpeningBalanceRes>> Update(int openingNo, OpeningBalanceReq reqModel)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var existing = await _dbContext.OpeningBalances
                    .Include(x => x.OpeningBalanceEntrys)
                    .FirstOrDefaultAsync(x => x.OpeningNo == openingNo);

                if (existing == null)
                {
                    return new Response<OpeningBalanceRes>
                    {
                        StatusMessage = "Opening balance not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                reqModel.Adapt(existing);

                existing.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
                existing.UpdationDate = DateTime.UtcNow.ToString("o");

                // Replace all entries
                _dbContext.Set<OpeningBalanceEntry>().RemoveRange(existing.OpeningBalanceEntrys);
                existing.OpeningBalanceEntrys = reqModel.OpeningBalanceEntrys?.Select(e => new OpeningBalanceEntry
                {
                    Id = Guid.NewGuid(),
                    BiltyNo = e.BiltyNo,
                    BiltyDate = e.BiltyDate,
                    VehicleNo = e.VehicleNo,
                    City = e.City,
                    Customer = e.Customer,
                    Broker = e.Broker,
                    ChargeType = e.ChargeType,
                    Debit = e.Debit,
                    Credit = e.Credit
                }).ToList() ?? new List<OpeningBalanceEntry>();

                await Repository.Update(existing, _ => existing);
                await UnitOfWork.SaveAsync();
                await transaction.CommitAsync();

                return new Response<OpeningBalanceRes>
                {
                    Data = existing.Adapt<OpeningBalanceRes>(),
                    StatusMessage = "Updated successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new Response<OpeningBalanceRes>
                {
                    StatusMessage = ex.InnerException?.Message ?? ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}