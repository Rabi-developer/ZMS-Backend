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
        Task<AccountOpeningBalanceStatus> UpdateStatusAsync(Guid id, string status);

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

        public override async Task<Response<AccountOpeningBalanceRes>> Update(AccountOpeningBalanceReq reqModel)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var existing = await _dbContext.AccountOpennigBalances
                    .Include(x => x.AccountOpeningBalanceEntrys)
                    .FirstOrDefaultAsync(x => x.Id == reqModel.Id);

                if (existing == null)
                {
                    return new Response<AccountOpeningBalanceRes>
                    {
                        StatusMessage = "Account opening balance not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                // ✅ Update only allowed parent fields (NOT whole entity)
                existing.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
                existing.UpdationDate = DateTime.UtcNow.ToString("o");

                _dbContext.Attach(existing);
                _dbContext.Entry(existing).Property(x => x.UpdatedBy).IsModified = true;
                _dbContext.Entry(existing).Property(x => x.UpdationDate).IsModified = true;


                // ✅ Prepare incoming ids
                var incomingIds = reqModel.AccountOpeningBalanceEntrys?
                    .Where(x => x.Id.HasValue && x.Id.Value != Guid.Empty)
                    .Select(x => x.Id.Value)
                    .ToHashSet()
                    ?? new HashSet<Guid>();


                // ✅ DELETE removed entries
                var toDelete = existing.AccountOpeningBalanceEntrys
                    .Where(db => !incomingIds.Contains((Guid)db.Id))
                    .ToList();

                _dbContext.Set<AccountOpeningBalanceEntry>().RemoveRange(toDelete);


                // ✅ UPDATE + INSERT
                foreach (var entryDto in reqModel.AccountOpeningBalanceEntrys ?? new List<AccountOpeningBalanceEntryReq>())
                {
                    AccountOpeningBalanceEntry dbItem = null;

                    if (entryDto.Id.HasValue && entryDto.Id.Value != Guid.Empty)
                    {
                        dbItem = existing.AccountOpeningBalanceEntrys
                            .FirstOrDefault(x => x.Id == entryDto.Id.Value);
                    }

                    if (dbItem != null)
                    {
                        // UPDATE existing
                        dbItem.Account = entryDto.Account;
                        dbItem.Debit = entryDto.Debit;
                        dbItem.Credit = entryDto.Credit;
                        dbItem.Narration = entryDto.Narration;

                        _dbContext.Entry(dbItem).State = EntityState.Modified;
                    }
                    else
                    {
                        // INSERT new
                        await _dbContext.Set<AccountOpeningBalanceEntry>().AddAsync(
                            new AccountOpeningBalanceEntry
                            {
                                Id = Guid.NewGuid(),
                                AccountOpeningBalanceId = existing.Id,

                                Account = entryDto.Account,
                                Debit = entryDto.Debit,
                                Credit = entryDto.Credit,
                                Narration = entryDto.Narration
                            });
                    }
                }

                // ✅ Single save
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return new Response<AccountOpeningBalanceRes>
                {
                    Data = existing.Adapt<AccountOpeningBalanceRes>(),
                    StatusMessage = "Account opening balance updated successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();

                return new Response<AccountOpeningBalanceRes>
                {
                    StatusMessage = e.InnerException?.Message ?? e.Message,
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

     
        public async Task<AccountOpeningBalanceStatus> UpdateStatusAsync(Guid id, string status)
        {
            if (status == null || id == Guid.Empty)
            {
                throw new ArgumentException("Contract ID and Status are required.");
            }

            var validStatuses = new[] { "Prepared", "Approved", "Canceled", "Closed", "UnApproved" };
            if (!validStatuses.Contains(status))
            {
                throw new ArgumentException($"Status must be one of: {string.Join(", ", validStatuses)}");
            }

            var accountOpennigBalances = await UnitOfWork._context.AccountOpennigBalances.Where(p => p.Id == id).FirstOrDefaultAsync();

            if (accountOpennigBalances == null)
            {
                throw new KeyNotFoundException($"EntryVoucher with ID {id} not found.");
            }

            accountOpennigBalances.Status = status;
            accountOpennigBalances.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
            accountOpennigBalances.UpdationDate = DateTime.UtcNow.ToString("o");

            await UnitOfWork.SaveAsync();

            return new AccountOpeningBalanceStatus
            {
                Id = id,
                Status = status
            };

        }
    }
}