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
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ZMS.Business.DTOs.Requests;
using ZMS.Domain.Entities;

namespace IMS.Business.Services;

public interface IConsignmentService : IBaseService<ConsignmentReq, ConsignmentRes, Consignment>
{
    public Task<ConsignmentStatus> UpdateStatusAsync(Guid id, string status);

}

public class ConsignmentService : BaseService<ConsignmentReq, ConsignmentRes, ConsignmentRepository, Consignment>, IConsignmentService
{
    private readonly IConsignmentRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public ConsignmentService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<ConsignmentRepository>();
        _context = context;
        _DbContext = dbContextn;
    }

    public async override Task<Response<IList<ConsignmentRes>>> GetAll(Pagination? paginate)
    {
        try
        {
            var pagination = paginate ?? new Pagination();
            //TODO: Get Pagination from the Query

            var (pag, data) = await Repository.GetAll(pagination, query => query.Include(p => p.Items));

            var OrderNo = await _DbContext.BookingOrder.ToListAsync();
            var Consignor = await _DbContext.Party.ToListAsync();
            var Consignee = await _DbContext.Party.ToListAsync();
            var SbrTax = await _DbContext.SalesTax.ToListAsync();



            var result = data.Adapt<List<ConsignmentRes>>();

            foreach (var item in result)
            {/*
                if (!string.IsNullOrWhiteSpace(item.OrderNo))
                    item.OrderNo = OrderNo.FirstOrDefault(t => t.Id.ToString() == item.OrderNo)?.OrderNo; // Adjust 'Name' to actual property*/

                if (!string.IsNullOrWhiteSpace(item.Consignor))
                    item.Consignor = Consignor.FirstOrDefault(v => v.Id.ToString() == item.Consignor)?.Name; // Adjust 'Name' to actual property
                if (!string.IsNullOrWhiteSpace(item.Consignee))
                    item.Consignee = Consignee.FirstOrDefault(v => v.Id.ToString() == item.Consignee)?.Name; // Adjust 'Name' to actual
                if (!string.IsNullOrWhiteSpace(item.SbrTax))
                    item.SbrTax = SbrTax.FirstOrDefault(v => v.Id.ToString() == item.SbrTax)?.TaxName; // Adjust 'Name' to actual
                                                                                                                  // 
            }
            return new Response<IList<ConsignmentRes>>
            {
                Data = result,
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<ConsignmentRes>>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async override Task<Response<ConsignmentRes>> Update(ConsignmentReq reqModel)
    {
        try
        {
            var existingEntity = await UnitOfWork._context.Consignment
                .Include(c => c.Items)
                .FirstOrDefaultAsync(p => p.Id == reqModel.Id);

            if (existingEntity == null)
            {
                return new Response<ConsignmentRes>
                {
                    StatusMessage = "Consignment not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            // Update main entity fields (but keep CreatedDate etc.)
            existingEntity.ConsignmentMode = reqModel.ConsignmentMode;
            existingEntity.OrderNo = reqModel.OrderNo;
            existingEntity.BiltyNo = reqModel.BiltyNo;
            existingEntity.Date = reqModel.Date;
            existingEntity.ConsignmentNo = reqModel.ConsignmentNo;
            existingEntity.Consignor = reqModel.Consignor;
            existingEntity.ConsignmentDate = reqModel.ConsignmentDate;
            existingEntity.CreditAllowed = reqModel.CreditAllowed;
            existingEntity.Consignee = reqModel.Consignee;
            existingEntity.ReceiverName = reqModel.ReceiverName;
            existingEntity.ReceiverContactNo = reqModel.ReceiverContactNo;
            existingEntity.ShippingLine = reqModel.ShippingLine;
            existingEntity.ContainerNo = reqModel.ContainerNo;
            existingEntity.Port = reqModel.Port;
            existingEntity.Destination = reqModel.Destination;
            existingEntity.FreightFrom = reqModel.FreightFrom;
            existingEntity.TotalQty = reqModel.TotalQty;
            existingEntity.Freight = reqModel.Freight;
            existingEntity.SbrTax = reqModel.SbrTax;
            existingEntity.SprAmount = reqModel.SprAmount;
            existingEntity.DeliveryCharges = reqModel.DeliveryCharges;
            existingEntity.InsuranceCharges = reqModel.InsuranceCharges;
            existingEntity.TollTax = reqModel.TollTax;
            existingEntity.OtherCharges = reqModel.OtherCharges;
            existingEntity.TotalAmount = reqModel.TotalAmount;
            existingEntity.ReceivedAmount = reqModel.ReceivedAmount;
            existingEntity.IncomeTaxDed = reqModel.IncomeTaxDed;
            existingEntity.IncomeTaxAmount = reqModel.IncomeTaxAmount;
            existingEntity.DeliveryDate = reqModel.DeliveryDate;
            existingEntity.Remarks = reqModel.Remarks;
            existingEntity.UpdatedBy = reqModel.UpdatedBy;
            existingEntity.UpdationDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            existingEntity.Status = reqModel.Status;

            // ✅ Handle Items
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
                            // Update tracked entity (no new instance created)
                            existingItem.Desc = reqItem.Desc;
                            existingItem.Qty = reqItem.Qty;
                            existingItem.Rate = reqItem.Rate;
                            existingItem.QtyUnit = reqItem.QtyUnit;
                            existingItem.Weight = reqItem.Weight;
                            existingItem.WeightUnit = reqItem.WeightUnit;
                        }
                        else
                        {
                            // Id provided but not in DB → add as new
                            existingEntity.Items.Add(new ConsignmentItem
                            {
                                Id = reqItem.Id.Value,
                                Desc = reqItem.Desc,
                                Qty = reqItem.Qty,
                                Rate = reqItem.Rate,
                                QtyUnit = reqItem.QtyUnit,
                                Weight = reqItem.Weight,
                                WeightUnit = reqItem.WeightUnit
                            });
                        }
                    }
                    else
                    {
                        // New item (no Id)
                        existingEntity.Items.Add(new ConsignmentItem
                        {
                            Id = Guid.NewGuid(),
                            Desc = reqItem.Desc,
                            Qty = reqItem.Qty,
                            Rate = reqItem.Rate,
                            QtyUnit = reqItem.QtyUnit,
                            Weight = reqItem.Weight,
                            WeightUnit = reqItem.WeightUnit
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
                    UnitOfWork._context.Remove(removeItem);
                }
            }

            await UnitOfWork.SaveAsync();

            return new Response<ConsignmentRes>
            {
                Data = existingEntity.Adapt<ConsignmentRes>(),
                StatusMessage = "Updated successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ConsignmentRes>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }


    public async virtual Task<Response<ConsignmentRes>> Get(Guid id)
    {
        try
        {
            var entity = await Repository.Get(id, query => query.Include(p => p.Items));
            if (entity == null)
            {
                return new Response<ConsignmentRes>
                {
                    StatusMessage = $"{typeof(Consignment).Name} Not found",
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            return new Response<ConsignmentRes>
            {
                Data = entity.Adapt<ConsignmentRes>(),
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ConsignmentRes>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
    public async virtual Task<Response<Guid>> Add(ConsignmentReq reqModel)
    {
        try
        {
            var entity = reqModel.Adapt<Consignment>();
            var entityAsBase = entity as IMinBase ??
                throw new InvalidOperationException(
                    "Conversion to IMinBase Failed. Make sure there's Id and CreatedDate properties.");

            // Add entity to repository
            var addedEntity = await Repository.Add((Consignment)entityAsBase);

            // Save changes to database
            await UnitOfWork.SaveAsync();

            // Get the actual ID after save - the tracked entity should have the correct ID
            // Access through IMinBase to get the base class Id property
            var savedId = ((IMinBase)addedEntity).Id;

            // If still empty, try to reload from database
            if (savedId == Guid.Empty)
            {
                // Reload the entity to get database-generated values
                await UnitOfWork._context.Entry(addedEntity).ReloadAsync();
                savedId = ((IMinBase)addedEntity).Id;
            }

            return new Response<Guid>
            {
                Data = savedId,
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


    public async Task<ConsignmentStatus> UpdateStatusAsync(Guid id, string status)
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

        var Consignment = await _DbContext.Consignment.Where(p => p.Id == id).FirstOrDefaultAsync();

        if (Consignment == null)
        {
            throw new KeyNotFoundException($"Consignment with ID {id} not found.");
        }

        Consignment.Status = status;
        Consignment.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
        Consignment.UpdationDate = DateTime.UtcNow.ToString("o");

        await UnitOfWork.SaveAsync();

        return new ConsignmentStatus
        {
            Id = id,
            Status = status,
        };
    }
}