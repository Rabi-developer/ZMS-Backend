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
    public interface IAccountOpeningBalanceService : IBaseService<AccountOpeningBalanceReq, AccountOpeningBalanceRes, AccountOpeningBalance>
    {
        Task<Response<IList<AccountOpeningBalanceRes>>> GetAll(Pagination? paginate);
        Task<Response<AccountOpeningBalanceRes>> Update(int accountOpeningNo, AccountOpeningBalanceReq reqModel);
        // Optional status update - uncomment if needed
        // Task<AccountOpeningBalanceStatus> UpdateStatusAsync(int accountOpeningNo, string status);
    }

    public class AccountOpeningBalanceService : BaseService<AccountOpeningBalanceReq, AccountOpeningBalanceRes, AccountOpeningBalanceRepository, AccountOpeningBalance>,
        IAccountOpeningBalanceService
    {
        private readonly IAccountOpeningBalanceRepository _repository; // assume you created this
        private readonly IHttpContextAccessor _context;
        private readonly ApplicationDbContext _dbContext;

        public AccountOpeningBalanceService(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor context,
            ApplicationDbContext dbContext)
            : base(unitOfWork)
        {
            _repository = unitOfWork.GetRepository<AccountOpeningBalanceRepository>();
            _context = context;
            _dbContext = dbContext;
        }

        public override async Task<Response<IList<AccountOpeningBalanceRes>>> GetAll(Pagination? paginate)
        {
            try
            {
                var pagination = paginate ?? new Pagination();
                var (pag, data) = await Repository.GetAll(pagination, q => q.Include(x => x.AccountOpeningBalanceEntrys));

                var result = data.Adapt<List<AccountOpeningBalanceRes>>();

                return new Response<IList<AccountOpeningBalanceRes>>
                {
                    Data = result,
                    Misc = pag,
                    StatusMessage = "Fetched successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new Response<IList<AccountOpeningBalanceRes>>
                {
                    StatusMessage = ex.InnerException?.Message ?? ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public override async Task<Response<Guid>> Add(AccountOpeningBalanceReq reqModel)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var entity = reqModel.Adapt<AccountOpeningBalance>();
                entity.Id = Guid.NewGuid();

                /*// Auto-increment AccountOpeningNo
                var lastNo = await _dbContext.AccountOpennigBalances
                    .MaxAsync(x => (int?)x.AccountOpeningNo) ?? 0;*/
               /* entity.AccountOpeningNo = lastNo + 1;*/

                entity.CreatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
                entity.CreationDate = DateTime.UtcNow.ToString("o");

                // Map entries
                entity.AccountOpeningBalanceEntrys = reqModel.AccountOpeningBalanceEntrys?.Select(e => new AccountOpeningBalanceEntry
                {
                    Id = Guid.NewGuid(),
                    Account = e.Account,
                    Debit = e.Debit,
                    Credit = e.Credit,
                    Narration = e.Narration
                }).ToList() ?? new List<AccountOpeningBalanceEntry>();

                await Repository.Add(entity);
                await UnitOfWork.SaveAsync();
                await transaction.CommitAsync();

                return new Response<Guid>
                {
                    Data = entity.Id,
                    StatusMessage = "Account opening balance created successfully",
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

        public override async Task<Response<AccountOpeningBalanceRes>> Get(Guid id)
        {
            try
            {
                var entity = await Repository.Get(id, q => q.Include(x => x.AccountOpeningBalanceEntrys));
                if (entity == null)
                {
                    return new Response<AccountOpeningBalanceRes>
                    {
                        StatusMessage = "Account opening balance not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                return new Response<AccountOpeningBalanceRes>
                {
                    Data = entity.Adapt<AccountOpeningBalanceRes>(),
                    StatusMessage = "Fetched successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new Response<AccountOpeningBalanceRes>
                {
                    StatusMessage = ex.InnerException?.Message ?? ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<AccountOpeningBalanceRes>> Update(int accountOpeningNo, AccountOpeningBalanceReq reqModel)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var existing = await _dbContext.AccountOpennigBalances
                    .Include(x => x.AccountOpeningBalanceEntrys)
                    .FirstOrDefaultAsync(x => x.AccountOpeningNo == accountOpeningNo);

                if (existing == null)
                {
                    return new Response<AccountOpeningBalanceRes>
                    {
                        StatusMessage = "Account opening balance not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                reqModel.Adapt(existing);

                existing.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
                existing.UpdationDate = DateTime.UtcNow.ToString("o");

                // Replace all entries (simplest & safest)
                _dbContext.Set<AccountOpeningBalanceEntry>().RemoveRange(existing.AccountOpeningBalanceEntrys);
                existing.AccountOpeningBalanceEntrys.Clear();

                if (reqModel.AccountOpeningBalanceEntrys != null)
                {
                    foreach (var entryDto in reqModel.AccountOpeningBalanceEntrys)
                    {
                        existing.AccountOpeningBalanceEntrys.Add(new AccountOpeningBalanceEntry
                        {
                            Id = Guid.NewGuid(),
                            Account = entryDto.Account,
                            Debit = entryDto.Debit,
                            Credit = entryDto.Credit,
                            Narration = entryDto.Narration
                        });
                    }
                }

                await Repository.Update(existing, _ => existing);
                await UnitOfWork.SaveAsync();
                await transaction.CommitAsync();

                return new Response<AccountOpeningBalanceRes>
                {
                    Data = existing.Adapt<AccountOpeningBalanceRes>(),
                    StatusMessage = "Account opening balance updated successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new Response<AccountOpeningBalanceRes>
                {
                    StatusMessage = ex.InnerException?.Message ?? ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        // Optional: Status update (uncomment if you want to use Status field)
        /*
        public async Task<AccountOpeningBalanceStatus> UpdateStatusAsync(int accountOpeningNo, string status)
        {
            var validStatuses = new[] { "Prepared", "Approved", "Canceled", "Closed", "UnApproved" };
            if (string.IsNullOrWhiteSpace(status) || !validStatuses.Contains(status))
            {
                throw new ArgumentException($"Invalid status. Allowed: {string.Join(", ", validStatuses)}");
            }

            var entity = await _dbContext.AccountOpeningBalance
                .FirstOrDefaultAsync(x => x.AccountOpeningNo == accountOpeningNo);

            if (entity == null)
            {
                throw new KeyNotFoundException($"AccountOpeningBalance #{accountOpeningNo} not found.");
            }

            entity.Status = status;
            entity.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
            entity.UpdationDate = DateTime.UtcNow.ToString("o");

            await UnitOfWork.SaveAsync();

            return new AccountOpeningBalanceStatus
            {
                AccountOpeningNo = accountOpeningNo,
                Status = status
            };
        }
        */
    }
}