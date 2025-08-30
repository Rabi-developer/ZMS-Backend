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
    public interface IEntryVoucherService : IBaseService<EntryVoucherReq, EntryVoucherRes, EntryVoucher>
    {
        Task<Response<IList<EntryVoucherRes>>> GetAll(Pagination? paginate);
        Task<EntryVoucherStatus> UpdateStatusAsync(Guid id, string status);
        Task<Response<EntryVoucherRes>> Update(Guid id, EntryVoucherReq reqModel);
    }

    public class EntryVoucherService : BaseService<EntryVoucherReq, EntryVoucherRes, EntryVoucherRepository, EntryVoucher>, IEntryVoucherService
    {
        private readonly IEntryVoucherRepository _repository;
        private readonly IHttpContextAccessor _context;
        private readonly ApplicationDbContext _DbContext;

        public EntryVoucherService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContext) : base(unitOfWork)
        {
            _repository = UnitOfWork.GetRepository<EntryVoucherRepository>();
            _context = context;
            _DbContext = dbContext;
        }

        public async override Task<Response<IList<EntryVoucherRes>>> GetAll(Pagination? paginate)
        {
            try
            {
                var pagination = paginate ?? new Pagination();
                var (pag, data) = await Repository.GetAll(pagination, query => query.Include(p => p.VoucherDetails));

                var result = data.Adapt<List<EntryVoucherRes>>();

                foreach (var item in result)
                {
                    if (item.VoucherDetails != null)
                    {
                        foreach (var detail in item.VoucherDetails)
                        {
                            if (!string.IsNullOrWhiteSpace(detail.Account1))
                            {
                                var account1 = await GetAccountById(detail.Account1);
                                detail.Account1 = account1?.Description ?? detail.Account1;
                            }
                            if (!string.IsNullOrWhiteSpace(detail.Account2))
                            {
                                var account2 = await GetAccountById(detail.Account2);
                                detail.Account2 = account2?.Description ?? detail.Account2;
                            }
                        }
                    }
                }

                return new Response<IList<EntryVoucherRes>>
                {
                    Data = result,
                    Misc = pag,
                    StatusMessage = "Fetch successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response<IList<EntryVoucherRes>>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async virtual Task<Response<Guid>> Add(EntryVoucherReq reqModel)
        {
            try
            {
                var entity = reqModel.Adapt<EntryVoucher>();
                entity.Id = Guid.NewGuid();

                var lastVoucher = await UnitOfWork._context.EntryVoucher
                    .OrderByDescending(p => p.Id)
                    .FirstOrDefaultAsync();

                if (lastVoucher == null || string.IsNullOrWhiteSpace(lastVoucher.VoucherNo))
                {
                    entity.VoucherNo = "1";
                }
                else
                {
                    int newNo = int.Parse(lastVoucher.VoucherNo) + 1;
                    entity.VoucherNo = newNo.ToString();
                }

                entity.CreatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
                entity.CreationDate = DateTime.UtcNow.ToString("o");

                // Calculate balances for VoucherDetails
                if (reqModel.VoucherDetails != null)
                {
                    entity.VoucherDetails = new List<VoucherDetail>();
                    foreach (var detailReq in reqModel.VoucherDetails)
                    {
                        var detail = detailReq.Adapt<VoucherDetail>();
                        detail.Id = Guid.NewGuid();

                        var account1 = await GetAccountById(detail.Account1);
                        var account2 = await GetAccountById(detail.Account2);

                        if (account1 == null || account2 == null)
                        {
                            throw new Exception("Invalid account ID(s) provided.");
                        }


                        detail.CurrentBalance1 = (float?)(account1.FixedAmount - account1.Paid);
                        detail.ProjectedBalance1 = detail.CurrentBalance1 + (detail.Debit1 ?? 0) - (detail.Credit1 ?? 0);
                        detail.CurrentBalance2 = (float?)(account2.FixedAmount - account2.Paid);
                        detail.ProjectedBalance2 = detail.CurrentBalance2 + (detail.Debit2 ?? 0) - (detail.Credit2 ?? 0);
                        /* if (detail.ProjectedBalance1 < 0 || detail.ProjectedBalance2 < 0)
                         {
                             throw new Exception($"Insufficient balance for account(s): {account1.Description}, {account2.Description}");
                         }*/

                        /*  account1.FixedAmount += (decimal)(detail.Debit1 ?? 0);
                          account1.Paid += (decimal)(detail.Credit1 ?? 0);
                          account2.FixedAmount += (decimal)(detail.Debit2 ?? 0);
                          account2.Paid += (decimal)(detail.Credit2 ?? 0);*/

                        await UpdateAccount(account1);
                        await UpdateAccount(account2);

                        entity.VoucherDetails.Add(detail);
                    }
                }

                var savedEntity = await Repository.Add((EntryVoucher)(entity as IMinBase ??
                    throw new InvalidOperationException("Conversion to IMinBase Failed. Make sure there's Id and CreatedDate properties.")));
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

        public async virtual Task<Response<EntryVoucherRes>> Get(Guid id)
        {
            try
            {
                var entity = await Repository.Get(id, query => query.Include(v => v.VoucherDetails));
                if (entity == null)
                {
                    return new Response<EntryVoucherRes>
                    {
                        StatusMessage = $"{typeof(EntryVoucher).Name} Not found",
                        StatusCode = HttpStatusCode.NoContent
                    };
                }

                var result = entity.Adapt<EntryVoucherRes>();
                if (result.VoucherDetails != null)
                {
                    foreach (var detail in result.VoucherDetails)
                    {
                        if (!string.IsNullOrWhiteSpace(detail.Account1))
                        {
                            var account1 = await GetAccountById(detail.Account1);
                            detail.Account1 = account1?.Description ?? detail.Account1;
                        }
                        if (!string.IsNullOrWhiteSpace(detail.Account2))
                        {
                            var account2 = await GetAccountById(detail.Account2);
                            detail.Account2 = account2?.Description ?? detail.Account2;
                        }
                    }
                }

                return new Response<EntryVoucherRes>
                {
                    Data = result,
                    StatusMessage = "Fetch successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response<EntryVoucherRes>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async virtual Task<Response<EntryVoucherRes>> Update(Guid id, EntryVoucherReq reqModel)
        {
            try
            {
                var existingEntity = await Repository.Get(id, query => query.Include(v => v.VoucherDetails));
                if (existingEntity == null)
                {
                    return new Response<EntryVoucherRes>
                    {
                        StatusMessage = $"{typeof(EntryVoucher).Name} Not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                // Map updated fields from request to existing entity
                reqModel.Adapt(existingEntity);
                existingEntity.Id = id;
                existingEntity.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
                existingEntity.UpdationDate = DateTime.UtcNow.ToString("o");

                // Remove existing VoucherDetails
                _DbContext.Set<VoucherDetail>().RemoveRange(existingEntity.VoucherDetails);
                existingEntity.VoucherDetails = new List<VoucherDetail>();

                // Process new VoucherDetails and calculate balances
                if (reqModel.VoucherDetails != null)
                {
                    foreach (var detailReq in reqModel.VoucherDetails)
                    {
                        var detail = detailReq.Adapt<VoucherDetail>();
                        detail.Id = Guid.NewGuid();

                        var account1 = await GetAccountById(detail.Account1);
                        var account2 = await GetAccountById(detail.Account2);

                        if (account1 == null || account2 == null)
                        {
                            throw new Exception("Invalid account ID(s) provided.");
                        }

                        detail.CurrentBalance1 = (float?)(account1.FixedAmount - account1.Paid);
                        detail.ProjectedBalance1 = detail.CurrentBalance1 + (detail.Debit1 ?? 0) - (detail.Credit1 ?? 0);
                        detail.CurrentBalance2 = (float?)(account2.FixedAmount - account2.Paid);
                        detail.ProjectedBalance2 = detail.CurrentBalance2 + (detail.Debit2 ?? 0) - (detail.Credit2 ?? 0);
                        /*if (detail.ProjectedBalance1 < 0 || detail.ProjectedBalance2 < 0)
                        {
                            throw new Exception($"Insufficient balance for account(s): {account1.Description}, {account2.Description}");
                        }*/

                        /* account1.FixedAmount += (decimal)(detail.Debit1 ?? 0);
                         account1.Paid += (decimal)(detail.Credit1 ?? 0);
                         account2.FixedAmount += (decimal)(detail.Debit2 ?? 0);
                         account2.Paid += (decimal)(detail.Credit2 ?? 0);*/

                        await UpdateAccount(account1);
                        await UpdateAccount(account2);

                        existingEntity.VoucherDetails.Add(detail);
                    }
                }

                await Repository.Update(existingEntity, e => { return existingEntity; });
                await UnitOfWork.SaveAsync();

                // Map to response DTO and enrich account names
                var result = existingEntity.Adapt<EntryVoucherRes>();
                if (result.VoucherDetails != null)
                {
                    foreach (var detail in result.VoucherDetails)
                    {
                        if (!string.IsNullOrWhiteSpace(detail.Account1))
                        {
                            var account1 = await GetAccountById(detail.Account1);
                            detail.Account1 = account1?.Description ?? detail.Account1;
                        }
                        if (!string.IsNullOrWhiteSpace(detail.Account2))
                        {
                            var account2 = await GetAccountById(detail.Account2);
                            detail.Account2 = account2?.Description ?? detail.Account2;
                        }
                    }
                }

                return new Response<EntryVoucherRes>
                {
                    Data = result,
                    StatusMessage = "Updated successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response<EntryVoucherRes>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<EntryVoucherStatus> UpdateStatusAsync(Guid id, string status)
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

            var entryVoucher = await _DbContext.EntryVoucher.Where(p => p.Id == id).FirstOrDefaultAsync();

            if (entryVoucher == null)
            {
                throw new KeyNotFoundException($"EntryVoucher with ID {id} not found.");
            }

            entryVoucher.Status = status;
            entryVoucher.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
            entryVoucher.UpdationDate = DateTime.UtcNow.ToString("o");

            await UnitOfWork.SaveAsync();

            return new EntryVoucherStatus
            {
                Id = id,
                Status = status
            };
        }

        private async Task<Account> GetAccountById(string accountId)
        {
            if (string.IsNullOrWhiteSpace(accountId) || !Guid.TryParse(accountId, out var guidId))
            {
                return null;
            }

            var asset = await _DbContext.Set<AblAssests>().FirstOrDefaultAsync(a => a.Id == guidId);
            if (asset != null)
            {
                return new Account
                {
                    Id = asset.Id.ToString(),
                    Description = asset.Description,
                    FixedAmount = decimal.TryParse(asset.FixedAmount, out var fixedAmount) ? fixedAmount : 0,
                    Paid = decimal.TryParse(asset.Paid, out var paid) ? paid : 0
                };
            }

            var liability = await _DbContext.Set<AblLiabilities>().FirstOrDefaultAsync(l => l.Id == guidId);
            if (liability != null)
            {
                return new Account
                {
                    Id = liability.Id.ToString(),
                    Description = liability.Description,
                    FixedAmount = decimal.TryParse(liability.FixedAmount, out var fixedAmount) ? fixedAmount : 0,
                    Paid = decimal.TryParse(liability.Paid, out var paid) ? paid : 0
                };
            }

            var revenue = await _DbContext.Set<AblRevenue>().FirstOrDefaultAsync(r => r.Id == guidId);
            if (revenue != null)
            {
                return new Account
                {
                    Id = revenue.Id.ToString(),
                    Description = revenue.Description,
                    FixedAmount = decimal.TryParse(revenue.FixedAmount, out var fixedAmount) ? fixedAmount : 0,
                    Paid = decimal.TryParse(revenue.Paid, out var paid) ? paid : 0
                };
            }

            var expense = await _DbContext.Set<AblExpense>().FirstOrDefaultAsync(e => e.Id == guidId);
            if (expense != null)
            {
                return new Account
                {
                    Id = expense.Id.ToString(),
                    Description = expense.Description,
                    FixedAmount = decimal.TryParse(expense.FixedAmount, out var fixedAmount) ? fixedAmount : 0,
                    Paid = decimal.TryParse(expense.Paid, out var paid) ? paid : 0
                };
            }

            var equity = await _DbContext.Set<Equality>().FirstOrDefaultAsync(e => e.Id == guidId);
            if (equity != null)
            {
                return new Account
                {
                    Id = equity.Id.ToString(),
                    Description = equity.Description,
                    FixedAmount = decimal.TryParse(equity.FixedAmount, out var fixedAmount) ? fixedAmount : 0,
                    Paid = decimal.TryParse(equity.Paid, out var paid) ? paid : 0
                };
            }

            return null;
        }

        private async Task UpdateAccount(Account account)
        {
            if (account == null || string.IsNullOrWhiteSpace(account.Id) || !Guid.TryParse(account.Id, out var guidId))
            {
                return;
            }

            var asset = await _DbContext.Set<AblAssests>().FirstOrDefaultAsync(a => a.Id == guidId);
            if (asset != null)
            {
                asset.FixedAmount = account.FixedAmount.ToString();
                asset.Paid = account.Paid.ToString();
                _DbContext.Update(asset);
                await _DbContext.SaveChangesAsync();
                return;
            }

            var liability = await _DbContext.Set<AblLiabilities>().FirstOrDefaultAsync(l => l.Id == guidId);
            if (liability != null)
            {
                liability.FixedAmount = account.FixedAmount.ToString();
                liability.Paid = account.Paid.ToString();
                _DbContext.Update(liability);
                await _DbContext.SaveChangesAsync();
                return;
            }

            var revenue = await _DbContext.Set<AblRevenue>().FirstOrDefaultAsync(r => r.Id == guidId);
            if (revenue != null)
            {
                revenue.FixedAmount = account.FixedAmount.ToString();
                revenue.Paid = account.Paid.ToString();
                _DbContext.Update(revenue);
                await _DbContext.SaveChangesAsync();
                return;
            }

            var expense = await _DbContext.Set<AblExpense>().FirstOrDefaultAsync(e => e.Id == guidId);
            if (expense != null)
            {
                expense.FixedAmount = account.FixedAmount.ToString();
                expense.Paid = account.Paid.ToString();
                _DbContext.Update(expense);
                await _DbContext.SaveChangesAsync();
                return;
            }

            var equity = await _DbContext.Set<Equality>().FirstOrDefaultAsync(e => e.Id == guidId);
            if (equity != null)
            {
                equity.FixedAmount = account.FixedAmount.ToString();
                equity.Paid = account.Paid.ToString();
                _DbContext.Update(equity);
                await _DbContext.SaveChangesAsync();
                return;
            }
        }

        private class Account
        {
            public string Id { get; set; }
            public string Description { get; set; }
            public decimal FixedAmount { get; set; }
            public decimal Paid { get; set; }
        }
    }
}