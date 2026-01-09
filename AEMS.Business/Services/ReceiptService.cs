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
using System.Net;
using System.Threading.Tasks;
using ZMS.Business.DTOs.Requests;
using ZMS.Domain.Entities;

namespace IMS.Business.Services;

public interface IReceiptService : IBaseService<ReceiptReq, ReceiptRes, Receipt>
{
    public Task<ReceiptStatus> UpdateStatusAsync(Guid id, string status);
    public Task<Response<BiltyBalanceRes>> GetBiltyBalance(string biltyNo);

}

public class ReceiptService : BaseService<ReceiptReq, ReceiptRes, ReceiptRepository, Receipt>, IReceiptService
{
    private readonly IReceiptRepository _repository; 
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public ReceiptService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<ReceiptRepository>();
        _context = context;
        _DbContext = dbContextn;
    }


    public async override Task<Response<IList<ReceiptRes>>> GetAllByUser(Pagination pagination, Guid userId, bool onlyusers = true)
    {
        try
        {
            var (pag, data) = await Repository.GetAllByUser(pagination, onlyusers, userId);

            var res = data.Adapt<List<ReceiptRes>>();

            var Party = await _DbContext.Party.ToListAsync();
/*            var Broker = await _DbContext.Brooker.ToListAsync();
*/


            var result = data.Adapt<List<ReceiptRes>>();

            foreach (var item in result)
            {
                if (!string.IsNullOrWhiteSpace(item.Party))
                    item.Party = Party.FirstOrDefault(t => t.Id.ToString() == item.Party)?.Name;
            }
            

            return new Response<IList<ReceiptRes>>
            {
                Data = result,
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<ReceiptRes>>
            {
                StatusMessage = e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    /*public async override Task<Response<Guid>> Add(ReceiptReq reqModel)
    {
        try
        {
            var entity = reqModel.Adapt<Receipt>();

            var GetlastNo = await UnitOfWork._context.Receipt
            .OrderByDescending(p => p.Id)
            .FirstOrDefaultAsync();

            if (GetlastNo.ReceiptNo == null || GetlastNo.ReceiptNo == "REC1758707358926542")
            {
                GetlastNo.ReceiptNo = "0";
            }
            
            else
            {
                int NewNo = int.Parse(GetlastNo.ReceiptNo) + 1;
                entity.ReceiptNo = NewNo.ToString();
            }

            var ss = await Repository.Add((Receipt)(entity as IMinBase ??
             throw new InvalidOperationException(
             "Conversion to IMinBase Failed. Make sure there's Id and CreatedDate properties.")));
            await UnitOfWork.SaveAsync();
            return new Response<Guid>
            {

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
    }*/

    public async virtual Task<Response<ReceiptRes>> Get(Guid id)
    {
        try
        {
            var entity = await Repository.Get(id, query => query.Include(p => p.Items));
            if (entity == null)
            {
                return new Response<ReceiptRes>
                {
                    StatusMessage = $"{typeof(Receipt).Name} Not found",
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            return new Response<ReceiptRes>
            {
                Data = entity.Adapt<ReceiptRes>(),
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ReceiptRes>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<ReceiptStatus> UpdateStatusAsync(Guid id, string status)
    {
        if (status == null || id == null)
        {
            throw new ArgumentException("Contract ID and Status are required.");
        }

        var validStatuses = new[] { "Prepared", "Approved", "Canceled", "Closed", "UnApproved" };
        if (!validStatuses.Contains(status))
        {
            throw new ArgumentException($"Status must be one of: {string.Join(", ", validStatuses)}");
        }

        var Receipt = await _DbContext.Receipt.Where(p => p.Id == id).FirstOrDefaultAsync();

        if (Receipt == null)
        {
            throw new KeyNotFoundException($"Receipt with ID {id} not found.");
        }

        Receipt.Status = status;
        Receipt.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
        Receipt.UpdationDate = DateTime.UtcNow.ToString("o");

        await UnitOfWork.SaveAsync();

        return new ReceiptStatus
        {
            Id = id,
            Status = status,
        };
    }
    public async Task<Response<BiltyBalanceRes>> GetBiltyBalance(string biltyNo)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(biltyNo))
            {
                return new Response<BiltyBalanceRes>
                {
                    StatusMessage = "Bilty number is required",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var cons = await _DbContext.Consignment.FirstOrDefaultAsync(c => c.BiltyNo == biltyNo);
            decimal totalAmount = 0;
            if (cons != null)
                totalAmount = Convert.ToDecimal(cons.TotalAmount ?? 0);

            if (totalAmount == 0)
            {
                var firstItem = await _DbContext.Receipt
                    .SelectMany(r => r.Items)
                    .FirstOrDefaultAsync(i => i.BiltyNo == biltyNo && i.TotalAmount != null);
                if (firstItem != null)
                    totalAmount = firstItem.TotalAmount ?? 0;
            }

            var totalReceived = await _DbContext.Receipt
                .SelectMany(r => r.Items)
                .Where(i => i.BiltyNo == biltyNo)
                .SumAsync(i => (decimal?)(i.ReceiptAmount ?? 0)) ?? 0;

            var balance = totalAmount - totalReceived;

            var res = new BiltyBalanceRes
            {
                BiltyNo = biltyNo,
                TotalAmount = totalAmount,
                ReceivedAmount = totalReceived,
                Balance = balance
            };

            return new Response<BiltyBalanceRes>
            {
                Data = res,
                StatusMessage = "Fetched successfully",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<BiltyBalanceRes>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            };
        }
    }

    public async override Task<Response<Guid>> Add(ReceiptReq reqModel)
    {
        try
        {
            if (reqModel.Items != null && reqModel.Items.Count > 0)
            {
                foreach (var it in reqModel.Items)
                {
                    var biltyNo = it.BiltyNo;
                    if (string.IsNullOrWhiteSpace(biltyNo))
                        continue;

                    var cons = await _DbContext.Consignment.FirstOrDefaultAsync(c => c.BiltyNo == biltyNo);
                    decimal totalAmount = 0;
                    if (cons != null)
                        totalAmount = Convert.ToDecimal(cons.TotalAmount ?? 0);

                    if (totalAmount == 0 && it.TotalAmount != null)
                        totalAmount = it.TotalAmount ?? 0;

                    var existingReceived = await _DbContext.Receipt
                        .SelectMany(r => r.Items)
                        .Where(i => i.BiltyNo == biltyNo)
                        .SumAsync(i => (decimal?)(i.ReceiptAmount ?? 0)) ?? 0;

                    var remainingBefore = totalAmount - existingReceived;

                    if (remainingBefore <= 0)
                    {
                        return new Response<Guid>
                        {
                            StatusMessage = $"Bilty {biltyNo} has zero remaining balance",
                            StatusCode = System.Net.HttpStatusCode.BadRequest
                        };
                    }

                    var receiptAmt = it.ReceiptAmount ?? 0;
                    if (receiptAmt > remainingBefore)
                    {
                        return new Response<Guid>
                        {
                            StatusMessage = $"Receipt amount {receiptAmt} exceeds remaining balance {remainingBefore} for bilty {biltyNo}",
                            StatusCode = System.Net.HttpStatusCode.BadRequest
                        };
                    }

                    it.TotalAmount = totalAmount;
                    it.Balance = remainingBefore - receiptAmt;
                   
                }
            }

            var entity = reqModel.Adapt<Receipt>();
            var entityAsBase = entity as IMinBase ??
                throw new InvalidOperationException(
                    "Conversion to IMinBase Failed. Make sure there's Id and CreatedDate properties.");

            var addedEntity = await Repository.Add((Receipt)entityAsBase);
            await UnitOfWork.SaveAsync();

            var savedId = ((IMinBase)addedEntity).Id;
            if (savedId == Guid.Empty)
            {
                await UnitOfWork._context.Entry(addedEntity).ReloadAsync();
                savedId = ((IMinBase)addedEntity).Id;
            }

            return new Response<Guid>
            {
                Data = savedId,
                StatusMessage = "Created successfully",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new Response<Guid>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            };
        }
    }
    public async Task<Response<ReceiptRes>> Update(ReceiptReq reqModel, int maxRetries = 3)
    {
        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            try
            {
                var existingEntity = await UnitOfWork._context.Receipt
                    .Include(r => r.Items)
                    .FirstOrDefaultAsync(p => p.Id == reqModel.Id);

                if (existingEntity == null)
                {
                    return new Response<ReceiptRes>
                    {
                        StatusMessage = "Receipt not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                // Validate input for duplicate item IDs
                if (reqModel.Items != null)
                {
                    var duplicateIds = reqModel.Items
                        .Where(i => i.Id.HasValue)
                        .GroupBy(i => i.Id.Value)
                        .Where(g => g.Count() > 1)
                        .Select(g => g.Key)
                        .ToList();

                    if (duplicateIds.Any())
                    {
                        return new Response<ReceiptRes>
                        {
                            StatusMessage = $"Duplicate item IDs detected: {string.Join(", ", duplicateIds)}",
                            StatusCode = HttpStatusCode.BadRequest
                        };
                    }
                }

                // Update main entity fields
/*                existingEntity.ReceiptNo = reqModel.ReceiptNo;
*/                existingEntity.ReceiptDate = reqModel.ReceiptDate;
                existingEntity.PaymentMode = reqModel.PaymentMode;
                existingEntity.BankName = reqModel.BankName;
                existingEntity.ChequeNo = reqModel.ChequeNo;
                existingEntity.ChequeDate = reqModel.ChequeDate;
                existingEntity.Party = reqModel.Party;
                existingEntity.ReceiptAmount = reqModel.ReceiptAmount;
                existingEntity.Remarks = reqModel.Remarks;
                existingEntity.SalesTaxOption = reqModel.SalesTaxOption;
                existingEntity.SalesTaxRate = reqModel.SalesTaxRate;
                existingEntity.WhtOnSbr = reqModel.WhtOnSbr;
                existingEntity.UpdatedBy = reqModel.UpdatedBy;
                existingEntity.UpdationDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
                existingEntity.Status = reqModel.Status;

                // Handle Items
                if (reqModel.Items != null)
                {
                    // 1. Update existing + add new
                    foreach (var reqItem in reqModel.Items)
                    {
                        if (reqItem.Id.HasValue)
                        {
                            var existingItem = existingEntity.Items
                                .FirstOrDefault(x => x.Id == reqItem.Id.Value);

                            if (existingItem != null)
                            {
                                // Update tracked entity
                                existingItem.BiltyNo = reqItem.BiltyNo;
                                existingItem.VehicleNo = reqItem.VehicleNo;
                                existingItem.BiltyDate = reqItem.BiltyDate;
                                existingItem.BiltyAmount = reqItem.BiltyAmount;
                                existingItem.SrbAmount = reqItem.SrbAmount;
                                existingItem.TotalAmount = reqItem.TotalAmount;
                                existingItem.Balance = reqItem.Balance;
                                existingItem.ReceiptAmount = reqItem.ReceiptAmount;
                            }
                            else
                            {
                                // Id provided but not in DB → add as new
                                existingEntity.Items.Add(new ReceiptItem
                                {
                                    Id = reqItem.Id.Value,
                                    BiltyNo = reqItem.BiltyNo,
                                    VehicleNo = reqItem.VehicleNo,
                                    BiltyDate = reqItem.BiltyDate,
                                    BiltyAmount = reqItem.BiltyAmount,
                                    SrbAmount = reqItem.SrbAmount,
                                    TotalAmount = reqItem.TotalAmount,
                                    Balance = reqItem.Balance,
                                    ReceiptAmount = reqItem.ReceiptAmount
                                });
                            }
                        }
                        else
                        {
                            // New item (no Id)
                            existingEntity.Items.Add(new ReceiptItem
                            {
                                Id = Guid.NewGuid(),
                                BiltyNo = reqItem.BiltyNo,
                                VehicleNo = reqItem.VehicleNo,
                                BiltyDate = reqItem.BiltyDate,
                                BiltyAmount = reqItem.BiltyAmount,
                                SrbAmount = reqItem.SrbAmount,
                                TotalAmount = reqItem.TotalAmount,
                                Balance = reqItem.Balance,
                                ReceiptAmount = reqItem.ReceiptAmount
                            });
                        }
                    }

                    // 2. Remove deleted items
                    var reqItemIds = reqModel.Items
                        .Where(i => i.Id.HasValue)
                        .Select(i => i.Id.Value)
                        .ToList();

                    var toRemove = existingEntity.Items
                        .Where(x => !reqItemIds.Contains((Guid)x.Id))
                        .ToList();

                    foreach (var removeItem in toRemove)
                    {
                        // Verify item exists before removing
                        if (await UnitOfWork._context.Receipt.AnyAsync(ri => ri.Id == removeItem.Id))
                        {
                            UnitOfWork._context.Remove(removeItem);
                        }
                    }
                }

                await UnitOfWork.SaveAsync();

                return new Response<ReceiptRes>
                {
                    Data = existingEntity.Adapt<ReceiptRes>(),
                    StatusMessage = "Updated successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (attempt == maxRetries)
                {
                    return new Response<ReceiptRes>
                    {
                        StatusMessage = "Failed to update due to concurrent modifications after multiple attempts. Please refresh and try again.",
                        StatusCode = HttpStatusCode.Conflict
                    };
                }
                // Wait before retrying to reduce contention
                await Task.Delay(100 * attempt);
            }
            catch (Exception e)
            {
                return new Response<ReceiptRes>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        return new Response<ReceiptRes>
        {
            StatusMessage = "Unexpected error occurred after multiple retry attempts.",
            StatusCode = HttpStatusCode.InternalServerError
        };
    }
}