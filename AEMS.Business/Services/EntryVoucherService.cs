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



        public override async Task<Response<IList<EntryVoucherRes>>> GetAll(Pagination? paginate)
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
                                // Recalculate latest balances if needed (uncomment if you want real-time instead of historical snapshot)
                                // detail.CurrentBalance1 = (float?)(account1.FixedAmount - account1.Paid);
                                // detail.ProjectedBalance1 = detail.CurrentBalance1 + (detail.Debit1 ?? 0) - (detail.Credit1 ?? 0);
                            }
                            if (!string.IsNullOrWhiteSpace(detail.Account2))
                            {
                                var account2 = await GetAccountById(detail.Account2);
                                detail.Account2 = account2?.Description ?? detail.Account2;
                                // detail.CurrentBalance2 = (float?)(account2.FixedAmount - account2.Paid);
                                // detail.ProjectedBalance2 = detail.CurrentBalance2 + (detail.Debit2 ?? 0) - (detail.Credit2 ?? 0);
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



        public override async Task<Response<EntryVoucherRes>> Get(Guid id)
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

        public override async Task<Response<Guid>> Add(EntryVoucherReq reqModel)
        {
            using var transaction = await _DbContext.Database.BeginTransactionAsync();
            try
            {
                var entity = reqModel.Adapt<EntryVoucher>();
                entity.Id = Guid.NewGuid();

                entity.CreatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
                entity.CreationDate = DateTime.UtcNow.ToString("o");

                // Process each VoucherDetail (each row)
                entity.VoucherDetails = new List<VoucherDetail>();

                if (reqModel.VoucherDetails != null)
                {
                    foreach (var detailReq in reqModel.VoucherDetails)
                    {
                        var detail = new VoucherDetail
                        {
                            Id = Guid.NewGuid(),
                            Account1 = detailReq.Account1,
                            Debit1 = detailReq.Debit1,
                            Credit1 = detailReq.Credit1,
                            Account2 = detailReq.Account2,
                            Debit2 = detailReq.Debit2,
                            Credit2 = detailReq.Credit2,
                            Narration = detailReq.Narration,
                            CurrentBalance1 = "0",
                            ProjectedBalance1 = "0",
                            CurrentBalance2 = "0",
                            ProjectedBalance2 = "0"
                        };

                        // Parse comma-separated values and update accounts
                        var account1Ids = detail.Account1?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
                        var debit1Values = detail.Debit1?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
                        var credit1Values = detail.Credit1?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();

                        for (int i = 0; i < account1Ids.Length; i++)
                        {
                            var accountId = account1Ids[i].Trim();
                            var debit = i < debit1Values.Length ? float.Parse(debit1Values[i]) : 0f;
                            var credit = i < credit1Values.Length ? float.Parse(credit1Values[i]) : 0f;

                            if (Guid.TryParse(accountId, out var guidId))
                            {
                                var account = await GetAccountById(accountId);
                                if (account != null)
                                {
                                    account.FixedAmount += debit;
                                    account.Paid += credit;
                                    await UpdateAccount(account);
                                }
                            }
                        }

                        var account2Ids = detail.Account2?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
                        var debit2Values = detail.Debit2?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
                        var credit2Values = detail.Credit2?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();

                        for (int i = 0; i < account2Ids.Length; i++)
                        {
                            var accountId = account2Ids[i].Trim();
                            var debit = i < debit2Values.Length ? float.Parse(debit2Values[i]) : 0f;
                            var credit = i < credit2Values.Length ? float.Parse(credit2Values[i]) : 0f;

                            if (Guid.TryParse(accountId, out var guidId))
                            {
                                var account = await GetAccountById(accountId);
                                if (account != null)
                                {
                                    account.FixedAmount += debit;
                                    account.Paid += credit;
                                    await UpdateAccount(account);
                                }
                            }
                        }

                        entity.VoucherDetails.Add(detail);
                    }
                }

                var savedEntity = await Repository.Add((EntryVoucher)(entity as IMinBase ??
                    throw new InvalidOperationException("Conversion to IMinBase Failed.")));

                await UnitOfWork.SaveAsync();
                await transaction.CommitAsync();

                return new Response<Guid>
                {
                    Data = entity.Id,
                    StatusMessage = "Created successfully",
                    StatusCode = HttpStatusCode.Created
                };
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                return new Response<Guid>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<EntryVoucherRes>> Update(Guid id, EntryVoucherReq reqModel)
        {
            using var transaction = await _DbContext.Database.BeginTransactionAsync();
            try
            {
                var existingEntity = await Repository.Get(id, query => query.Include(v => v.VoucherDetails));
                if (existingEntity == null)
                {
                    return new Response<EntryVoucherRes>
                    {
                        StatusMessage = "Entry Voucher not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                // Reverse old VoucherDetails effects
                if (existingEntity.VoucherDetails != null)
                {
                    foreach (var oldDetail in existingEntity.VoucherDetails)
                    {
                        // Reverse Account 1
                        var account1Ids = oldDetail.Account1?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
                        var debit1Values = oldDetail.Debit1?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
                        var credit1Values = oldDetail.Credit1?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();

                        for (int i = 0; i < account1Ids.Length; i++)
                        {
                            var accountId = account1Ids[i].Trim();
                            var debit = i < debit1Values.Length ? float.Parse(debit1Values[i]) : 0f;
                            var credit = i < credit1Values.Length ? float.Parse(credit1Values[i]) : 0f;

                            if (Guid.TryParse(accountId, out var guidId))
                            {
                                var account = await GetAccountById(accountId);
                                if (account != null)
                                {
                                    account.FixedAmount -= debit;
                                    account.Paid -= credit;
                                    await UpdateAccount(account);
                                }
                            }
                        }

                        // Reverse Account 2
                        var account2Ids = oldDetail.Account2?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
                        var debit2Values = oldDetail.Debit2?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
                        var credit2Values = oldDetail.Credit2?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();

                        for (int i = 0; i < account2Ids.Length; i++)
                        {
                            var accountId = account2Ids[i].Trim();
                            var debit = i < debit2Values.Length ? float.Parse(debit2Values[i]) : 0f;
                            var credit = i < credit2Values.Length ? float.Parse(credit2Values[i]) : 0f;

                            if (Guid.TryParse(accountId, out var guidId))
                            {
                                var account = await GetAccountById(accountId);
                                if (account != null)
                                {
                                    account.FixedAmount -= debit;
                                    account.Paid -= credit;
                                    await UpdateAccount(account);
                                }
                            }
                        }
                    }
                }

                // Update main voucher properties
                existingEntity.VoucherDate = reqModel.VoucherDate;
                existingEntity.ReferenceNo = reqModel.ReferenceNo;
                existingEntity.ChequeNo = reqModel.ChequeNo;
                existingEntity.DepositSlipNo = reqModel.DepositSlipNo;
                existingEntity.PaymentMode = reqModel.PaymentMode;
                existingEntity.BankName = reqModel.BankName;
                existingEntity.ChequeDate = reqModel.ChequeDate;
                existingEntity.PaidTo = reqModel.PaidTo;
                existingEntity.Narration = reqModel.Narration;
                existingEntity.Description = reqModel.Description;
                existingEntity.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
                existingEntity.UpdationDate = DateTime.UtcNow.ToString("o");

                // Remove old details
                if (existingEntity.VoucherDetails != null)
                {
                    _DbContext.Set<VoucherDetail>().RemoveRange(existingEntity.VoucherDetails);
                }

                // Add new details
                existingEntity.VoucherDetails = new List<VoucherDetail>();

                if (reqModel.VoucherDetails != null)
                {
                    foreach (var detailReq in reqModel.VoucherDetails)
                    {
                        var detail = new VoucherDetail
                        {
                            Id = Guid.NewGuid(),
                            Account1 = detailReq.Account1,
                            Debit1 = detailReq.Debit1,
                            Credit1 = detailReq.Credit1,
                            Account2 = detailReq.Account2,
                            Debit2 = detailReq.Debit2,
                            Credit2 = detailReq.Credit2,
                            Narration = detailReq.Narration,
                            CurrentBalance1 = "0",
                            ProjectedBalance1 = "0",
                            CurrentBalance2 = "0",
                            ProjectedBalance2 = "0"
                        };

                        // Parse and apply Account 1
                        var account1Ids = detail.Account1?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
                        var debit1Values = detail.Debit1?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
                        var credit1Values = detail.Credit1?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();

                        for (int i = 0; i < account1Ids.Length; i++)
                        {
                            var accountId = account1Ids[i].Trim();
                            var debit = i < debit1Values.Length ? float.Parse(debit1Values[i]) : 0f;
                            var credit = i < credit1Values.Length ? float.Parse(credit1Values[i]) : 0f;

                            if (Guid.TryParse(accountId, out var guidId))
                            {
                                var account = await GetAccountById(accountId);
                                if (account != null)
                                {
                                    account.FixedAmount += debit;
                                    account.Paid += credit;
                                    await UpdateAccount(account);
                                }
                            }
                        }

                        // Parse and apply Account 2
                        var account2Ids = detail.Account2?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
                        var debit2Values = detail.Debit2?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
                        var credit2Values = detail.Credit2?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();

                        for (int i = 0; i < account2Ids.Length; i++)
                        {
                            var accountId = account2Ids[i].Trim();
                            var debit = i < debit2Values.Length ? float.Parse(debit2Values[i]) : 0f;
                            var credit = i < credit2Values.Length ? float.Parse(credit2Values[i]) : 0f;

                            if (Guid.TryParse(accountId, out var guidId))
                            {
                                var account = await GetAccountById(accountId);
                                if (account != null)
                                {
                                    account.FixedAmount += debit;
                                    account.Paid += credit;
                                    await UpdateAccount(account);
                                }
                            }
                        }

                        existingEntity.VoucherDetails.Add(detail);
                    }
                }

                await Repository.Update(existingEntity, e => existingEntity);
                await UnitOfWork.SaveAsync();
                await transaction.CommitAsync();

                var result = existingEntity.Adapt<EntryVoucherRes>();

                return new Response<EntryVoucherRes>
                {
                    Data = result,
                    StatusMessage = "Updated successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
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
                    FixedAmount = float.TryParse(asset.FixedAmount, out var fixedAmount) ? fixedAmount : 0, // Changed to float
                    Paid = float.TryParse(asset.Paid, out var paid) ? paid : 0 // Changed to float
                };
            }

            var liability = await _DbContext.Set<AblLiabilities>().FirstOrDefaultAsync(l => l.Id == guidId);
            if (liability != null)
            {
                return new Account
                {
                    Id = liability.Id.ToString(),
                    Description = liability.Description,
                    FixedAmount = float.TryParse(liability.FixedAmount, out var fixedAmount) ? fixedAmount : 0, // Changed to float
                    Paid = float.TryParse(liability.Paid, out var paid) ? paid : 0 // Changed to float
                };
            }

            var revenue = await _DbContext.Set<AblRevenue>().FirstOrDefaultAsync(r => r.Id == guidId);
            if (revenue != null)
            {
                return new Account
                {
                    Id = revenue.Id.ToString(),
                    Description = revenue.Description,
                    FixedAmount = float.TryParse(revenue.FixedAmount, out var fixedAmount) ? fixedAmount : 0, // Changed to float
                    Paid = float.TryParse(revenue.Paid, out var paid) ? paid : 0 // Changed to float
                };
            }

            var expense = await _DbContext.Set<AblExpense>().FirstOrDefaultAsync(e => e.Id == guidId);
            if (expense != null)
            {
                return new Account
                {
                    Id = expense.Id.ToString(),
                    Description = expense.Description,
                    FixedAmount = float.TryParse(expense.FixedAmount, out var fixedAmount) ? fixedAmount : 0, // Changed to float
                    Paid = float.TryParse(expense.Paid, out var paid) ? paid : 0 // Changed to float
                };
            }

            var equity = await _DbContext.Set<Equality>().FirstOrDefaultAsync(e => e.Id == guidId);
            if (equity != null)
            {
                return new Account
                {
                    Id = equity.Id.ToString(),
                    Description = equity.Description,
                    FixedAmount = float.TryParse(equity.FixedAmount, out var fixedAmount) ? fixedAmount : 0, // Changed to float
                    Paid = float.TryParse(equity.Paid, out var paid) ? paid : 0 // Changed to float
                };
            }

            return null;
        }
        public async override Task<Response<IList<EntryVoucherRes>>> GetAllByUser(Pagination pagination, Guid userId, bool onlyusers = true)
        {
            try
            {
                var (pag, data) = await Repository.GetAllByUser(pagination, onlyusers, userId);

                var res = data.Adapt<List<EntryVoucherRes>>();

                var PaidTo = await _DbContext.BusinessAssociate.ToListAsync();
                var Broker = await _DbContext.Brooker.ToListAsync();




                var result = data.Adapt<List<EntryVoucherRes>>();

                foreach (var item in result)
                {
                    if (!string.IsNullOrWhiteSpace(item.PaidTo))
                        item.PaidTo = PaidTo.FirstOrDefault(v => v.Id.ToString() == item.PaidTo)?.Name;
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
                    StatusMessage = e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
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
                return;
            }

            var liability = await _DbContext.Set<AblLiabilities>().FirstOrDefaultAsync(l => l.Id == guidId);
            if (liability != null)
            {
                liability.FixedAmount = account.FixedAmount.ToString();
                liability.Paid = account.Paid.ToString();
                _DbContext.Update(liability);
                return;
            }

            var revenue = await _DbContext.Set<AblRevenue>().FirstOrDefaultAsync(r => r.Id == guidId);
            if (revenue != null)
            {
                revenue.FixedAmount = account.FixedAmount.ToString();
                revenue.Paid = account.Paid.ToString();
                _DbContext.Update(revenue);
                return;
            }

            var expense = await _DbContext.Set<AblExpense>().FirstOrDefaultAsync(e => e.Id == guidId);
            if (expense != null)
            {
                expense.FixedAmount = account.FixedAmount.ToString();
                expense.Paid = account.Paid.ToString();
                _DbContext.Update(expense);
                return;
            }

            var equity = await _DbContext.Set<Equality>().FirstOrDefaultAsync(e => e.Id == guidId);
            if (equity != null)
            {
                equity.FixedAmount = account.FixedAmount.ToString();
                equity.Paid = account.Paid.ToString();
                _DbContext.Update(equity);
                return;
            }
        }

        private class Account
        {
            public string Id { get; set; }
            public string Description { get; set; }
            public float FixedAmount { get; set; } // Changed to float
            public float Paid { get; set; } // Changed to float
        }
    }
}