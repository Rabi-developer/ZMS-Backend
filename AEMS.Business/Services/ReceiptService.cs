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

                    // First, try to get from Consignment
                    var cons = await _DbContext.Consignment.FirstOrDefaultAsync(c => c.BiltyNo == biltyNo);
                    decimal totalAmount = 0;

                    if (cons != null)
                    {
                        totalAmount = Convert.ToDecimal(cons.TotalAmount ?? 0);
                    }

                    // If not found or zero in Consignment, check OpeningBalance
                    if (totalAmount == 0)
                    {
                        var obEntry = await _DbContext.OpeningBalances
                            .SelectMany(ob => ob.OpeningBalanceEntrys)
                            .FirstOrDefaultAsync(e => e.BiltyNo == biltyNo);

                        if (obEntry != null)
                            totalAmount = Convert.ToDecimal(obEntry.Debit ?? 0); // Or Debit-Credit depending on your logic
                    }

                    // If the user provided amount, override totalAmount if still zero
                    if (totalAmount == 0 && it.TotalAmount != null)
                        totalAmount = it.TotalAmount ?? 0;

                    // Check existing receipts for this bilty
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
            var GetlastNo = await UnitOfWork._context.Receipt.AddAsync(entity);
            await UnitOfWork.SaveAsync();
            return new Response<Guid>
            {

                StatusMessage = "Added successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new Response<Guid>
            {
                StatusMessage = ex.Message,
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            };
        }
    }

    public async override Task<Response<ReceiptRes>> Update(ReceiptReq reqModel)
    {
        try
        {
         
            var existing = await UnitOfWork._context.Receipt
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == reqModel.Id);

            if (existing == null)
            {
                return new Response<ReceiptRes>
                {
                    StatusMessage = "Receipt not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

          
            UnitOfWork._context.Receipt.Attach(existing);
            UnitOfWork._context.Entry(existing).State = EntityState.Modified;

         
            var originalReceiptNo = existing.ReceiptNo;

         
            existing.ReceiptDate = reqModel.ReceiptDate;
            existing.PaymentMode = reqModel.PaymentMode;
            existing.BankName = reqModel.BankName;
            existing.ChequeNo = reqModel.ChequeNo;
            existing.ChequeDate = reqModel.ChequeDate;
            existing.Party = reqModel.Party;
            existing.ReceiptAmount = reqModel.ReceiptAmount;
            existing.Remarks = reqModel.Remarks;
            existing.SalesTaxOption = reqModel.SalesTaxOption;
            existing.SalesTaxRate = reqModel.SalesTaxRate;
            existing.WhtOnSbr = reqModel.WhtOnSbr;
            existing.UpdatedBy = reqModel.UpdatedBy;
            existing.UpdationDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            existing.Status = reqModel.Status;

           
            existing.ReceiptNo = originalReceiptNo;
            UnitOfWork._context.Entry(existing).Property(e => e.ReceiptNo).IsModified = false;

            if (reqModel.Items?.Any() == true)
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

           
            await UnitOfWork._context.Set<ReceiptItem>()
                .Where(i => i.ReceiptId == reqModel.Id)
                .ExecuteDeleteAsync();

          
            if (reqModel.Items?.Any() == true)
            {
                foreach (var reqItem in reqModel.Items)
                {
                    var newItem = new ReceiptItem
                    {
                        Id = Guid.NewGuid(),
                        ReceiptId = reqModel.Id,                   
                        BiltyNo = reqItem.BiltyNo,
                        VehicleNo = reqItem.VehicleNo,
                        BiltyDate = reqItem.BiltyDate,
                        BiltyAmount = reqItem.BiltyAmount,
                        SrbAmount = reqItem.SrbAmount,
                        TotalAmount = reqItem.TotalAmount,
                        Balance = reqItem.Balance,
                        ReceiptAmount = reqItem.ReceiptAmount
                    };

                    UnitOfWork._context.Set<ReceiptItem>().Add(newItem);
                }
            }

         
            await UnitOfWork.SaveAsync();

          
            var refreshed = await UnitOfWork._context.Receipt
                .Include(r => r.Items)
                .FirstOrDefaultAsync(r => r.Id == reqModel.Id);

            return new Response<ReceiptRes>
            {
                Data = refreshed?.Adapt<ReceiptRes>(),
                StatusMessage = "Updated successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (DbUpdateConcurrencyException)
        {
            return new Response<ReceiptRes>
            {
                StatusMessage = "Update failed due to concurrent modification or deletion. Please refresh the receipt and try again.",
                StatusCode = HttpStatusCode.Conflict
            };
        }
        catch (Exception ex)
        {
            return new Response<ReceiptRes>
            {
                Data = null,
                StatusMessage = ex.InnerException?.Message ?? ex.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}