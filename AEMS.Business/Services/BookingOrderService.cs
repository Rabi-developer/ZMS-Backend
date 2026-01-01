using IMS.Business.DTOs.Requests;
using ZMS.Business.DTOs.Responses;
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
using System.Linq;
using System.Collections.Generic;

namespace IMS.Business.Services;

public interface IBookingOrderService : IBaseService<BookingOrderReq, BookingOrderRes, BookingOrder>
{
    Task<Response<IList<BookingOrderRes>>> GetAll(Pagination? paginate);
    Task<BookingOrderStatus> UpdateStatusAsync(Guid id, string status);
    Task<Response<IList<RelatedConsignmentRes>>> GetConsignmentsByBookingOrderIdAsync(Guid bookingOrderId, bool includeDetails = false);
    Task<Response<Guid>> AddConsignmentAsync(Guid bookingOrderId, RelatedConsignmentReq reqModel);
    Task<Response<Guid>> UpdateConsignmentAsync(Guid bookingOrderId, Guid consignmentId, RelatedConsignmentReq reqModel);
    Task<Response<bool>> DeleteConsignmentAsync(Guid bookingOrderId, Guid consignmentId);
    Task<Response<BookingOrderRes>> UpdateAsync(Guid id, BookingOrderReq reqModel);
    Task<Response<IList<OrderProgressRes>>> GetOrderProgressAsync(Guid bookingOrderId);
}

public class BookingOrderService : BaseService<BookingOrderReq, BookingOrderRes, BookingOrderRepository, BookingOrder>, IBookingOrderService
{
    private readonly IBookingOrderRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public BookingOrderService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<BookingOrderRepository>();
        _context = context;
        _DbContext = dbContextn;


    }



public async override Task<Response<IList<BookingOrderRes>>> GetAll(Pagination? paginate)
    {
        try
        {
            var pagination = paginate ?? new Pagination();
            var (pag, data) = await Repository.GetAll(pagination, null);

            var transporters = await _DbContext.Transporters.ToListAsync();
            var vendors = await _DbContext.Vendor.ToListAsync();

            var result = data.Adapt<List<BookingOrderRes>>();

            foreach (var item in result)
            {
                if (!string.IsNullOrWhiteSpace(item.Transporter))
                    item.Transporter = transporters.FirstOrDefault(t => t.Id.ToString() == item.Transporter)?.Name;

                if (!string.IsNullOrWhiteSpace(item.Vendor))
                    item.Vendor = vendors.FirstOrDefault(v => v.Id.ToString() == item.Vendor)?.Name;
            }
            return new Response<IList<BookingOrderRes>>
            {
                Data = result,
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<BookingOrderRes>>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async override Task<Response<Guid>> Add(BookingOrderReq reqModel)
    {
        try
        {
            var entity = reqModel.Adapt<BookingOrder>();


            var ss = await Repository.Add((BookingOrder)(entity as IMinBase ??
                throw new InvalidOperationException(
                "Conversion to IMinBase Failed. Make sure there's Id and CreatedDate properties.")));
            await UnitOfWork.SaveAsync();
            return new Response<Guid>
            {
                Data = ss.Id,
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

    public async override Task<Response<BookingOrderRes>> Get(Guid id)
    {
        try
        {
            var entity = await Repository.Get(id, null);
            if (entity == null)
            {
                return new Response<BookingOrderRes>
                {
                    StatusMessage = $"{typeof(BookingOrder).Name} Not found",
                    StatusCode = HttpStatusCode.NoContent
                };
            }

            var result = entity.Adapt<BookingOrderRes>();
            var transporters = await _DbContext.Transporters.ToListAsync();
            var vendors = await _DbContext.Vendor.ToListAsync();

          /*  if (!string.IsNullOrWhiteSpace(result.Transporter))
                result.Transporter = transporters.FirstOrDefault(t => t.Id.ToString() == result.Transporter)?.Name;

            if (!string.IsNullOrWhiteSpace(result.Vendor))
                result.Vendor = vendors.FirstOrDefault(v => v.Id.ToString() == result.Vendor)?.Name;*/

            return new Response<BookingOrderRes>
            {
                Data = result,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<BookingOrderRes>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<BookingOrderStatus> UpdateStatusAsync(Guid id, string status)
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

        var bookingOrder = await _DbContext.BookingOrder.Where(p => p.Id == id).FirstOrDefaultAsync();

        if (bookingOrder == null)
        {
            throw new KeyNotFoundException($"BookingOrder with ID {id} not found.");
        }

        bookingOrder.Status = status;
        bookingOrder.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
        bookingOrder.UpdationDate = DateTime.UtcNow.ToString("o");

        await UnitOfWork.SaveAsync();

        return new BookingOrderStatus
        {
            Id = id,
            Status = status,
        };
    }

    public async Task<Response<IList<RelatedConsignmentRes>>> GetConsignmentsByBookingOrderIdAsync(Guid bookingOrderId, bool includeDetails = false)
    {
        try
        {
            var bookingOrder = await _DbContext.BookingOrder.FirstOrDefaultAsync(b => b.Id == bookingOrderId);
            if (bookingOrder == null)
            {
                return new Response<IList<RelatedConsignmentRes>>
                {
                    StatusMessage = $"BookingOrder with ID {bookingOrderId} not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var consignments = await _DbContext.RelatedConsignments
                .Where(c => c.BookingOrderId == bookingOrderId)
                .ToListAsync();
            var result = consignments.Adapt<List<RelatedConsignmentRes>>();

            // BookingOrder OrderNo as string for matching
            var bookingOrderNo = bookingOrder.OrderNo.ToString();

            if (includeDetails && result != null && result.Count > 0)
            {
                // Attempt to enrich each related consignment with data from Consignment table
                foreach (var rc in result)
                {
                    try
                    {
                        Consignment? cons = null;

                        // Try match by ReceiptNo (RelatedConsignment stores as string)
                        if (!string.IsNullOrWhiteSpace(rc.ReceiptNo) && int.TryParse(rc.ReceiptNo, out var receiptNo))
                        {
                            cons = await _DbContext.Consignment.FirstOrDefaultAsync(x => x.ReceiptNo == receiptNo);
                        }

                        // If not found by receipt, try match by BiltyNo
                        if (cons == null && !string.IsNullOrWhiteSpace(rc.BiltyNo))
                        {
                            cons = await _DbContext.Consignment.FirstOrDefaultAsync(x => x.BiltyNo == rc.BiltyNo);
                        }

                        // If not found yet, try matching by BookingOrder.OrderNo -> Consignment.OrderNo
                        if (cons == null && !string.IsNullOrWhiteSpace(bookingOrderNo))
                        {
                            cons = await _DbContext.Consignment.FirstOrDefaultAsync(x => x.OrderNo == bookingOrderNo);
                        }

                        if (cons != null)
                        {
                            // populate fields from consignment
                            rc.BiltyNo = string.IsNullOrWhiteSpace(cons.BiltyNo) ? rc.BiltyNo : cons.BiltyNo;
                            rc.ReceiptNo = (cons.ReceiptNo).ToString();
                            rc.Consignor = string.IsNullOrWhiteSpace(cons.Consignor) ? rc.Consignor : cons.Consignor;
                            rc.Consignee = string.IsNullOrWhiteSpace(cons.Consignee) ? rc.Consignee : cons.Consignee;
                            // Map item/qty/amounts
                            rc.Item = string.IsNullOrWhiteSpace(rc.Item) ? null : rc.Item; // keep existing if present
                            if (cons.Items != null && cons.Items.Count > 0)
                            {
                                // if RelatedConsignment has no item/qty, try to fill from first item
                                rc.Item ??= cons.Items.First().Desc;
                                if ((rc.Qty == null || rc.Qty == 0) && cons.Items.First().Qty.HasValue)
                                    rc.Qty = (int?)Convert.ToInt32(cons.Items.First().Qty.Value);
                            }

                            rc.TotalAmount = cons.TotalAmount != null ? (decimal?)Convert.ToDecimal(cons.TotalAmount) : rc.TotalAmount;
                            rc.RecvAmount = cons.ReceivedAmount != null ? (decimal?)Convert.ToDecimal(cons.ReceivedAmount) : rc.RecvAmount;
                            rc.DelDate = string.IsNullOrWhiteSpace(cons.DeliveryDate) ? rc.DelDate : cons.DeliveryDate;
                            rc.Status = string.IsNullOrWhiteSpace(cons.Status) ? rc.Status : cons.Status;
                        }
                    }
                    catch
                    {
                        // ignore enrichment errors per-item
                        continue;
                    }
                }
            }

            return new Response<IList<RelatedConsignmentRes>>
            {
                Data = result,
                StatusMessage = "Fetched successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<RelatedConsignmentRes>>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<Response<Guid>> AddConsignmentAsync(Guid bookingOrderId, RelatedConsignmentReq reqModel)
    {
        try
        {
            var bookingOrderExists = await _DbContext.BookingOrder.AnyAsync(b => b.Id == bookingOrderId);
            if (!bookingOrderExists)
            {
                return new Response<Guid>
                {
                    StatusMessage = $"BookingOrder with ID {bookingOrderId} not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var ConsignmentGet = await _DbContext.Consignment.Where(p=>p.Id == reqModel.ConsignmentId).FirstOrDefaultAsync();
            if (ConsignmentGet ==null)
            {
                return new Response<Guid>
                {
                    StatusMessage = $"Consignment Not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }
            


            var entity = reqModel.Adapt<RelatedConsignment>();
            entity.BookingOrderId = bookingOrderId;

            entity.ReceiptNo = ConsignmentGet.ReceiptNo.ToString();

            entity.BiltyNo = ConsignmentGet.BiltyNo;
            entity.RecvAmount = (decimal)ConsignmentGet.ReceivedAmount;
            
            entity.ModifiedDateTime = DateTime.UtcNow;

            _DbContext.RelatedConsignments.Add(entity);
            await UnitOfWork.SaveAsync();

            return new Response<Guid>
            {
                Data = entity.Id,
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

    public async Task<Response<Guid>> UpdateConsignmentAsync(Guid bookingOrderId, Guid consignmentId, RelatedConsignmentReq reqModel)
    {
        try
        {
            var bookingOrderExists = await _DbContext.BookingOrder.AnyAsync(b => b.Id == bookingOrderId);
            if (!bookingOrderExists)
            {
                return new Response<Guid>
                {
                    StatusMessage = $"BookingOrder with ID {bookingOrderId} not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var entity = await _DbContext.RelatedConsignments
                .FirstOrDefaultAsync(c => c.Id == consignmentId && c.BookingOrderId == bookingOrderId);

            if (entity == null)
            {
                // Create new consignment if not found
                entity = reqModel.Adapt<RelatedConsignment>();
                entity.Id = consignmentId;
                entity.BookingOrderId = bookingOrderId;
                // entity.CreatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
                entity.CreatedDateTime = DateTime.UtcNow;
              //  entity.ModifiedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
                entity.ModifiedDateTime = DateTime.UtcNow;

                _DbContext.RelatedConsignments.Add(entity);
                await UnitOfWork.SaveAsync();

                return new Response<Guid>
                {
                    Data = entity.Id,
                    StatusMessage = "Created successfully",
                    StatusCode = HttpStatusCode.Created
                };
            }

            // Update existing consignment
            reqModel.Adapt(entity);
            entity.BookingOrderId = bookingOrderId;
          //  entity.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
            entity.ModifiedDateTime = DateTime.UtcNow;

            _DbContext.RelatedConsignments.Update(entity);
            await UnitOfWork.SaveAsync();

            return new Response<Guid>
            {
                Data = entity.Id,
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

    public async Task<Response<bool>> DeleteConsignmentAsync(Guid bookingOrderId, Guid consignmentId)
    {
        try
        {
            var bookingOrderExists = await _DbContext.BookingOrder.AnyAsync(b => b.Id == bookingOrderId);
            if (!bookingOrderExists)
            {
                return new Response<bool>
                {
                    StatusMessage = $"BookingOrder with ID {bookingOrderId} not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var entity = await _DbContext.RelatedConsignments
                .FirstOrDefaultAsync(c => c.Id == consignmentId && c.BookingOrderId == bookingOrderId);
            if (entity == null)
            {
                return new Response<bool>
                {
                    StatusMessage = $"RelatedConsignment with ID {consignmentId} not found for BookingOrder {bookingOrderId}",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            _DbContext.RelatedConsignments.Remove(entity);
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
   
    public async Task<Response<BookingOrderRes>> UpdateAsync(Guid id, BookingOrderReq reqModel)
    {
        try
        {
            // 1. Get existing entity
            var entity = await Repository.Get(id, null);
            if (entity == null)
            {
                return new Response<BookingOrderRes>
                {
                    StatusMessage = "BookingOrder not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            // 2. Map DTO → Entity (including Files!)
            reqModel.Adapt(entity);

            // 3. Update audit fields
            entity.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
            entity.UpdationDate = DateTime.UtcNow.ToString("o");

            // 4. Save
            await UnitOfWork.SaveAsync();

            // 5. Convert to response DTO
            var result = entity.Adapt<BookingOrderRes>();

            // 6. Resolve Transporter & Vendor names (optional but good)
            var transporters = await _DbContext.Transporters.ToListAsync();
            var vendors = await _DbContext.Vendor.ToListAsync();

            if (!string.IsNullOrWhiteSpace(result.Transporter))
                result.Transporter = transporters.FirstOrDefault(t => t.Id.ToString() == result.Transporter)?.Name;

            if (!string.IsNullOrWhiteSpace(result.Vendor))
                result.Vendor = vendors.FirstOrDefault(v => v.Id.ToString() == result.Vendor)?.Name;

            // 7. RETURN THE RESPONSE
            return new Response<BookingOrderRes>
            {
                Data = result,
                StatusMessage = "Booking Order updated successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<BookingOrderRes>
            {
                StatusMessage = e.InnerException?.Message ?? e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<Response<IList<OrderProgressRes>>> GetOrderProgressAsync(Guid bookingOrderId)
    {
        try
        {
            var bookingOrder = await _DbContext.BookingOrder.FirstOrDefaultAsync(b => b.Id == bookingOrderId);
            if (bookingOrder == null)
            {
                return new Response<IList<OrderProgressRes>>
                {
                    StatusMessage = $"BookingOrder with ID {bookingOrderId} not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            // get related consignments
            var related = await _DbContext.RelatedConsignments.Where(r => r.BookingOrderId == bookingOrderId).ToListAsync();

            var results = new List<OrderProgressRes>();

            // Preload payments and charges for order matching
            var orderNoStr = bookingOrder.OrderNo.ToString();

            var payments = await _DbContext.PaymentABL
                .Include(p => p.PaymentABLItem)
                .Where(p => p.PaymentABLItem.Any(i => i.OrderNo == orderNoStr))
                .ToListAsync();

            var chargesForOrder = await _DbContext.Charges
                .Include(c => c.Lines)
                .Include(c => c.Payments)
                .Where(c => c.OrderNo == orderNoStr)
                .ToListAsync();

            // sum of paid amounts from charges payments
            var totalPaidFromCharges = chargesForOrder.SelectMany(c => c.Payments ?? new List<ChargesPayments>())
                .Where(p => p.PaidAmount.HasValue)
                .Sum(p => p.PaidAmount) ;

            // preload lookup tables
            var parties = await _DbContext.Party.ToListAsync();
            var businessAssociates = await _DbContext.BusinessAssociate.ToListAsync();
            var munshyana = await _DbContext.Munshyana.ToListAsync();
            var transporters = await _DbContext.Transporters.ToListAsync();
            var vendors = await _DbContext.Vendor.ToListAsync();

            foreach (var rel in related)
            {
                var op = new OrderProgressRes();
                op.Id = rel.Id;

                // Try to locate Consignment by ReceiptNo, BiltyNo, or OrderNo and include items
                Consignment? cons = null;
                if (!string.IsNullOrWhiteSpace(rel.ReceiptNo) && int.TryParse(rel.ReceiptNo, out var recNo))
                {
                    cons = await _DbContext.Consignment.Include(c => c.Items).FirstOrDefaultAsync(c => c.ReceiptNo == recNo);
                }
                if (cons == null && !string.IsNullOrWhiteSpace(rel.BiltyNo))
                {
                    cons = await _DbContext.Consignment.Include(c => c.Items).FirstOrDefaultAsync(c => c.BiltyNo == rel.BiltyNo);
                }
                if (cons == null)
                {
                    cons = await _DbContext.Consignment.Include(c => c.Items).FirstOrDefaultAsync(c => c.OrderNo == orderNoStr && (string.IsNullOrWhiteSpace(rel.BiltyNo) || c.BiltyNo == rel.BiltyNo));
                }

                // Fill basic fields from related record and consignment
                op.biltyNo = cons?.BiltyNo ?? rel.BiltyNo;
                op.receiptNo = cons != null ? cons.ReceiptNo.ToString() : rel.ReceiptNo;
                op.orderNo = bookingOrder.OrderNo.ToString();
                op.orderDate = bookingOrder.OrderDate;
                op.vehicleNo = bookingOrder.VehicleNo;

                // resolve consignor/consignee names if values are GUIDs referencing Party or BusinessAssociate or Vendor/Transporter
                string ResolveEntityName(string? idOrValue)
                {
                    if (string.IsNullOrWhiteSpace(idOrValue)) return string.Empty;
                    // if looks like GUID, try lookups
                    if (Guid.TryParse(idOrValue, out var gid))
                    {
                        var p = parties.FirstOrDefault(x => x.Id == gid);
                        if (p != null && !string.IsNullOrWhiteSpace(p.Name)) return p.Name;
                        var ba = businessAssociates.FirstOrDefault(x => x.Id == gid);
                        if (ba != null && !string.IsNullOrWhiteSpace(ba.Name)) return ba.Name;
                        var t = transporters.FirstOrDefault(x => x.Id == gid);
                        if (t != null && !string.IsNullOrWhiteSpace(t.Name)) return t.Name;
                        var v = vendors.FirstOrDefault(x => x.Id == gid);
                        if (v != null && !string.IsNullOrWhiteSpace(v.Name)) return v.Name;
                    }
                    return idOrValue;
                }

                op.consignor = ResolveEntityName(cons?.Consignor ?? rel.Consignor);
                op.consignee = ResolveEntityName(cons?.Consignee ?? rel.Consignee);

                // items and qty from consignment items when available
                if (cons?.Items != null && cons.Items.Count > 0)
                {
                    op.items = string.Join(", ", cons.Items.Select(i => i.Desc).Where(x => !string.IsNullOrWhiteSpace(x)));
                    var totalQty = cons.Items.Where(i => i.Qty.HasValue).Sum(i => (int)Convert.ToInt32(i.Qty.Value));
                    op.qty = totalQty.ToString();
                }
                else if (!string.IsNullOrWhiteSpace(rel.Item))
                {
                    op.items = rel.Item;
                    op.qty = rel.Qty?.ToString() ?? "0";
                }

                op.totalAmount = cons?.TotalAmount != null ? cons.TotalAmount.ToString() : (rel.TotalAmount?.ToString() ?? "0");
                op.receivedAmount = cons?.ReceivedAmount != null ? cons.ReceivedAmount.ToString() : (rel.RecvAmount?.ToString() ?? "0");
                op.deliveryDate = cons?.DeliveryDate ?? rel.DelDate;
                op.consignmentStatus = cons?.Status ?? rel.Status;

                // Paid Amount: sum from Charges.Payments for this order
                op.paidAmount = (totalPaidFromCharges != null ? totalPaidFromCharges.ToString() : "0");

                // Charges and Amount - filter lines that match this consignment biltyNo
                var matchingChargeLines = chargesForOrder.SelectMany(c => c.Lines ?? new List<ChargeLine>())
                    .Where(l => string.IsNullOrWhiteSpace(l.BiltyNo) || (!string.IsNullOrWhiteSpace(op.biltyNo) && l.BiltyNo == op.biltyNo))
                    .ToList();

                if (matchingChargeLines.Any())
                {
                    // map charge id -> Munshyana.ChargesDesc when possible, and collect amounts
                    var chargeNames = new List<string>();
                    var chargeAmounts = new List<string>();
                    foreach (var cl in matchingChargeLines)
                    {
                        string chargeName = cl.Charge ?? string.Empty;
                        if (!string.IsNullOrWhiteSpace(cl.Charge) && Guid.TryParse(cl.Charge, out var chargeGuid))
                        {
                            var m = munshyana.FirstOrDefault(x => x.Id == chargeGuid);
                            if (m != null && !string.IsNullOrWhiteSpace(m.ChargesDesc)) chargeName = m.ChargesDesc;
                        }
                        chargeNames.Add(chargeName);

                        var amt = cl.Amount.HasValue ? cl.Amount.Value.ToString("0.##") : "0";
                        chargeAmounts.Add(amt);
                    }

                    op.charges = string.Join(", ", chargeNames);
                    op.amount = string.Join(", ", chargeAmounts);

                    // determine paidToPerson: prefer PaidTo from charge lines, resolve name
                    var paidToId = matchingChargeLines.Select(l => l.PaidTo).FirstOrDefault(x => !string.IsNullOrWhiteSpace(x));
                    if (!string.IsNullOrWhiteSpace(paidToId))
                        op.paidToPerson = ResolveEntityName(paidToId);
                }
                else
                {
                    // fallback: first charge line across all charges for order
                    var firstChargeLine = chargesForOrder.SelectMany(c => c.Lines ?? new List<ChargeLine>()).FirstOrDefault();
                    if (firstChargeLine != null)
                    {
                        op.paidToPerson = ResolveEntityName(firstChargeLine.PaidTo);
                    }
                }

                // PaymentNo - find PaymentABL that has an item with same orderNo
                var payment = payments.SelectMany(p => p.PaymentABLItem ?? new List<PaymentABLItem>())
                    .FirstOrDefault(i => i.OrderNo == orderNoStr || (!string.IsNullOrWhiteSpace(op.biltyNo) && i.OrderNo == op.biltyNo));
                if (payment != null)
                {
                    var parentPayment = payments.FirstOrDefault(p => p.PaymentABLItem != null && p.PaymentABLItem.Any(it => it.OrderNo == payment.OrderNo));
                    if (parentPayment != null)
                        op.paymentNo = parentPayment.PaymentNo.ToString();
                }

                results.Add(op);
            }

            return new Response<IList<OrderProgressRes>>
            {
                Data = results,
                StatusMessage = "Fetched successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<OrderProgressRes>>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }


}