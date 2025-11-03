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

public interface IBookingOrderService : IBaseService<BookingOrderReq, BookingOrderRes, BookingOrder>
{
    Task<Response<IList<BookingOrderRes>>> GetAll(Pagination? paginate);
    Task<BookingOrderStatus> UpdateStatusAsync(Guid id, string status);
    Task<Response<IList<RelatedConsignmentRes>>> GetConsignmentsByBookingOrderIdAsync(Guid bookingOrderId);
    Task<Response<Guid>> AddConsignmentAsync(Guid bookingOrderId, RelatedConsignmentReq reqModel);
    Task<Response<Guid>> UpdateConsignmentAsync(Guid bookingOrderId, Guid consignmentId, RelatedConsignmentReq reqModel);
    Task<Response<bool>> DeleteConsignmentAsync(Guid bookingOrderId, Guid consignmentId);
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

    //public async override Task<Response<Guid>> Add(BookingOrderReq reqModel)
    //{
    //    try
    //    {
    //        var entity = reqModel.Adapt<BookingOrder>();

    //        var GetlastNo = await UnitOfWork._context.BookingOrder
    //            .OrderByDescending(p => p.Id)
    //            .FirstOrDefaultAsync();

    //        if (GetlastNo == null || GetlastNo.OrderNo == "")
    //        {
    //            entity.OrderNo = "1";
    //        }
    //        else
    //        {
    //            int NewNo = int.Parse(GetlastNo.OrderNo) + 1;
    //            entity.OrderNo = NewNo.ToString();
    //        }

    //        var ss = await Repository.Add((BookingOrder)(entity as IMinBase ??
    //            throw new InvalidOperationException(
    //            "Conversion to IMinBase Failed. Make sure there's Id and CreatedDate properties.")));
    //        await UnitOfWork.SaveAsync();
    //        return new Response<Guid>
    //        {
    //            Data = ss.Id ?? Guid.Empty,
    //            StatusMessage = "Created successfully",
    //            StatusCode = HttpStatusCode.Created
    //        };
    //    }
    //    catch (Exception e)
    //    {
    //        return new Response<Guid>
    //        {
    //            StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
    //            StatusCode = HttpStatusCode.InternalServerError
    //        };
    //    }
    //}

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

    public async Task<Response<IList<RelatedConsignmentRes>>> GetConsignmentsByBookingOrderIdAsync(Guid bookingOrderId)
    {
        try
        {
            var bookingOrderExists = await _DbContext.BookingOrder.AnyAsync(b => b.Id == bookingOrderId);
            if (!bookingOrderExists)
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

            var entity = reqModel.Adapt<RelatedConsignment>();
            entity.BookingOrderId = bookingOrderId;
         //   entity.CreatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
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
}