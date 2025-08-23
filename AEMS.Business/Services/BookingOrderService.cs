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
    public Task<BookingOrderStatus> UpdateStatusAsync(Guid id, string status);

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
            //TODO: Get Pagination from the Query

            var (pag, data) = await Repository.GetAll(pagination, query => query.Include(p => p.Consignments));

            var transporters = await _DbContext.Transporters.ToListAsync();
            var vendors = await _DbContext.Vendor.ToListAsync();

            var result = data.Adapt<List<BookingOrderRes>>();

            foreach (var item in result)
            {
                if (!string.IsNullOrWhiteSpace(item.Transporter))
                    item.Transporter = transporters.FirstOrDefault(t => t.Id.ToString() == item.Transporter)?.Name; // Adjust 'Name' to actual property

                if (!string.IsNullOrWhiteSpace(item.Vendor))
                    item.Vendor = vendors.FirstOrDefault(v => v.Id.ToString() == item.Vendor)?.Name; // Adjust 'Name' to actual property
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

    public async virtual Task<Response<Guid>> Add(BookingOrderReq reqModel)
    {
        try
        {
            var entity = reqModel.Adapt<BookingOrder>();

            var GetlastNo = await UnitOfWork._context.BookingOrder
     .OrderByDescending(p => p.Id)
     .FirstOrDefaultAsync();

            if (GetlastNo == null || GetlastNo.OrderNo == "")
            {
                entity.OrderNo = "1";
            }
            else
            {
                int NewNo = int.Parse(GetlastNo.OrderNo) + 1;
                entity.OrderNo = NewNo.ToString();
            }

            var ss = await Repository.Add((BookingOrder)(entity as IMinBase ??
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
    }


    public async virtual Task<Response<BookingOrderRes>> Get(Guid id)
    {
        try
        {
            var entity = await Repository.Get(id, query => query.Include(p => p.Consignments));
            if (entity == null)
            {
                return new Response<BookingOrderRes>
                {
                    StatusMessage = $"{typeof(BookingOrder).Name} Not found",
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            return new Response<BookingOrderRes>
            {
                Data = entity.Adapt<BookingOrderRes>(),
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

        var BookingOrder = await _DbContext.BookingOrder.Where(p => p.Id == id).FirstOrDefaultAsync();

        if (BookingOrder == null)
        {
            throw new KeyNotFoundException($"BookingOrder with ID {id} not found.");
        }

        BookingOrder.Status = status;
        BookingOrder.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
        BookingOrder.UpdationDate = DateTime.UtcNow.ToString("o");

        await UnitOfWork.SaveAsync();

        return new BookingOrderStatus
        {
            Id = id,
            Status = status,
        };
    }
}