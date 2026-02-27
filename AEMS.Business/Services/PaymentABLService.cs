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

public interface IPaymentABLService : IBaseService<PaymentABLReq, PaymentABLRes, PaymentABL>
{
    public Task<PaymentAblStatus> UpdateStatusAsync(Guid id, string status);
    public Task<Response<ChargeHistory>> HistoryPayment (string VehicleNo, string OrderNo , string Charges, bool IsOpeningBalance = false);

}

public class PaymentABLService : BaseService<PaymentABLReq, PaymentABLRes, PaymentABLRepository, PaymentABL>, IPaymentABLService
{
    private readonly IPaymentABLRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public PaymentABLService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<PaymentABLRepository>();
        _context = context;
        _DbContext = dbContextn;
    }


    public async override Task<Response<PaymentABLRes>> Update(PaymentABLReq reqModel)
    {
        try
        {
            // Adapt request model to entity
            var entity = reqModel.Adapt<PaymentABL>();

            // Get the existing payment with its items
            var existingEntity = await Repository.Get(entity.Id, query=> query.Include(p=> p.PaymentABLItem));
            if (existingEntity == null)
            {
                return new Response<PaymentABLRes>
                {
                    StatusMessage = "Payment record not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            // Handle child items (PaymentABLItem)
            if (reqModel.PaymentABLItem != null && reqModel.PaymentABLItem.Any())
            {
                // Get IDs of incoming items
                var incomingItemIds = reqModel.PaymentABLItem
                    .Where(x => x.Id.HasValue && x.Id != Guid.Empty)
                    .Select(x => x.Id.Value)
                    .ToList();

                // Remove items that are not in the incoming request
                var itemsToRemove = existingEntity.PaymentABLItem
                    ?.Where(x => !incomingItemIds.Contains((Guid)x.Id))
                    .ToList() ?? new List<PaymentABLItem>();

                foreach (var item in itemsToRemove)
                {
                    existingEntity.PaymentABLItem?.Remove(item);
                }

                // Add or update items
                foreach (var itemReq in reqModel.PaymentABLItem)
                {
                    if (!itemReq.Id.HasValue || itemReq.Id == Guid.Empty)
                    {
                        // New item
                        var newItem = itemReq.Adapt<PaymentABLItem>();
                        existingEntity.PaymentABLItem?.Add(newItem);
                    }
                    else
                    {
                        // Update existing item
                        var existingItem = existingEntity.PaymentABLItem?
                            .FirstOrDefault(x => x.Id == itemReq.Id);

                        if (existingItem != null)
                        {
                            // Map fields from request to existing item
                            existingItem.VehicleNo = itemReq.VehicleNo;
                            existingItem.OrderNo = itemReq.OrderNo;
                            existingItem.Charges = itemReq.Charges;
                            existingItem.OrderDate = itemReq.OrderDate;
                            existingItem.DueDate = itemReq.DueDate;
                            existingItem.ExpenseAmount = itemReq.ExpenseAmount;
                            existingItem.Balance = itemReq.Balance;
                            existingItem.PaidAmount = itemReq.PaidAmount;
                        }
                    }
                }
            }
            else
            {
                // No items provided - clear all
                existingEntity.PaymentABLItem?.Clear();
            }

            // Update the main entity
            var res = await Repository.Update(existingEntity, null);
            await UnitOfWork.SaveAsync();

            return new Response<PaymentABLRes>
            {
                Data = res.Adapt<PaymentABLRes>(),
                StatusMessage = "Updated successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<PaymentABLRes>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async override Task<Response<IList<PaymentABLRes>>> GetAllByUser(Pagination pagination, Guid userId, bool onlyusers = true)
    {
        try
        {
            var (pag, data) = await Repository.GetAllByUser(pagination, onlyusers, userId);

            var res = data.Adapt<List<PaymentABLRes>>();

            var PaidTo = await _DbContext.BusinessAssociate.ToListAsync();
            var Broker = await _DbContext.Brooker.ToListAsync();




            var result = data.Adapt<List<PaymentABLRes>>();

            foreach (var item in result)
            {
                if (!string.IsNullOrWhiteSpace(item.PaidTo))
                    item.PaidTo = PaidTo.FirstOrDefault(v => v.Id.ToString() == item.PaidTo)?.Name;
            }

            return new Response<IList<PaymentABLRes>>
            {
                Data = result,
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<PaymentABLRes>>
            {
                StatusMessage = e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }



    /*  public async override Task<Response<Guid>> Add(PaymentABLReq reqModel)
      {
          try
          {
              var entity = reqModel.Adapt<PaymentABL>();

              var GetlastNo = await UnitOfWork._context.PaymentABL
       .OrderByDescending(p => p.Id)
       .FirstOrDefaultAsync();

              if (GetlastNo == null || GetlastNo.PaymentNo == "" || GetlastNo.PaymentNo == "REC516552277")
              {
                  entity.PaymentNo = "1";
              }
              else
              {
                  int NewNo = int.Parse(GetlastNo.PaymentNo) + 1;
                  entity.PaymentNo = NewNo.ToString();
              }

              var ss = await Repository.Add((PaymentABL)(entity as IMinBase ??
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

    public async override Task<Response<PaymentABLRes>> Get(Guid id)
    {
        try
        {
            var entity = await Repository.Get(id, query => query.Include(p => p.PaymentABLItem));
            if (entity == null)
            {
                return new Response<PaymentABLRes>
                {
                    StatusMessage = $"{typeof(PaymentABL).Name} Not found",
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            return new Response<PaymentABLRes>
            {
                Data = entity.Adapt<PaymentABLRes>(),
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<PaymentABLRes>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<PaymentAblStatus> UpdateStatusAsync(Guid id, string status)
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

        var PaymentABL = await _DbContext.PaymentABL.Where(p => p.Id == id).FirstOrDefaultAsync();

        if (PaymentABL == null)
        {
            throw new KeyNotFoundException($"PaymentABL with ID {id} not found.");
        }

        PaymentABL.Status = status;
        PaymentABL.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
        PaymentABL.UpdationDate = DateTime.UtcNow.ToString("o");

        await UnitOfWork.SaveAsync();

        return new PaymentAblStatus
        {
            Id = id,
            Status = status,
        };
    }

    public async Task<Response<ChargeHistory>> HistoryPayment(string VehicleNo, string OrderNo, string Charges, bool IsOpeningBalance = false)
    {
        string? chargeGuidString = null;

        if (int.TryParse(Charges, out int chargeNo))
        {
            chargeGuidString = await _DbContext.Charges
                .Where(c => c.ChargeNo == chargeNo)
                .SelectMany(c => c.Lines)
                .Select(l => l.Charge)
                .FirstOrDefaultAsync();
        }
        else if (Guid.TryParse(Charges, out Guid parsedGuid))
        {
            chargeGuidString = parsedGuid.ToString();
        }

        if (string.IsNullOrEmpty(chargeGuidString))
        {
            return new Response<ChargeHistory>
            {
                Data = null,
                StatusMessage = "Charge line not found",
                StatusCode = HttpStatusCode.OK
            };
        }
        var history = new ChargeHistory();
        if (IsOpeningBalance == false) { 
            history = await (
                from p in _DbContext.PaymentABL where 
                p.IsDeleted != true
                from i in p.PaymentABLItem
                where i.VehicleNo == VehicleNo
                      && i.OrderNo == OrderNo
                      && i.Charges == chargeGuidString
                orderby p.CreatedDateTime descending
                select new ChargeHistory
                {
                    Id = p.Id,
                    VehicleNo = i.VehicleNo,
                    OrderNo = i.OrderNo,
                    Charges = Charges,
                    Balance = i.Balance,
                    PaidAmount = i.PaidAmount
                }
            ).FirstOrDefaultAsync(); }

        else
        {
            history = await (
               from p in _DbContext.PaymentABL
               where
                p.IsDeleted != true
               from i in p.PaymentABLItem
               where i.VehicleNo == VehicleNo
                     && i.OrderNo == OrderNo && p.IsDeleted != true
               orderby p.CreatedDateTime descending
               select new ChargeHistory 
               {
                   Id = p.Id,
                   VehicleNo = i.VehicleNo,
                   OrderNo = i.OrderNo,
                   Charges = Charges,
                   Balance = i.Balance,
                   PaidAmount = i.PaidAmount
               }
           ).FirstOrDefaultAsync();
        }
   
                                                                                  

        return new Response<ChargeHistory>
        {
            Data = history,
            StatusMessage = "Fetch successfully",
            StatusCode = HttpStatusCode.OK
        };
    }
}