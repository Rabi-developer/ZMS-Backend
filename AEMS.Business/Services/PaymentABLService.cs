﻿using IMS.Business.DTOs.Requests;
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
    public Task<PaymentABLService> UpdateStatusAsync(Guid id, string status);

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


    public async override Task<Response<IList<PaymentABLRes>>> GetAll(Pagination? paginate)
    {
        try
        {
            var pagination = paginate ?? new Pagination();
            //TODO: Get Pagination from the Query

            var (pag, data) = await Repository.GetAll(pagination, query => query.Include(p => p.PaymentABLItem));
           /* var charges = await _DbContext.Charges
             .Include(c => c.Lines)
             .ToListAsync();

            var result = data.Adapt<List<PaymentABLRes>>();

            foreach (var item in result)
            {
                foreach (var line in item.PaymentABLItem)
                {
                    if (!string.IsNullOrWhiteSpace(line.Charges))
                    {
                        var chargeLine = charges.SelectMany(c => c.Lines)
                            .FirstOrDefault(cl => cl.Id.ToString() == line.Charges);
                        if (chargeLine != null)
                        {
                            line.Charges = chargeLine.Charge;
                        }
                    }
                }
            }*/
            return new Response<IList<PaymentABLRes>>
            {
                Data = data.Adapt<List<PaymentABLRes>>(),
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<PaymentABLRes>>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }


    public async override Task<Response<Guid>> Add(PaymentABLReq reqModel)
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
    }

    public async virtual Task<Response<PaymentABLRes>> Get(Guid id)
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

    Task<PaymentABLService> IPaymentABLService.UpdateStatusAsync(Guid id, string status)
    {
        throw new NotImplementedException();
    }
}