﻿using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Base;
using IMS.Domain.Context;
using IMS.Domain.Entities;
using Mapster;
using System.Net;

namespace IMS.Business.Services;

public interface IBuyerService : IBaseService<BuyerReq, BuyerRes, Buyer>
{

}
public class BuyerService : BaseService<BuyerReq, BuyerRes, BuyerRepository, Buyer>, IBuyerService
{
    private readonly ApplicationDbContext _context;

    public BuyerService(IUnitOfWork unitOfWork, ApplicationDbContext context) : base(unitOfWork)
    {
        _context = context;
    }


    public override async  Task<Response<Guid>> Add(BuyerReq reqModel)
    {
        try
        {
            var getaccount = _context.Assets.Where(p => p.Description == reqModel.BuyerName).FirstOrDefault();
            if (getaccount == null)
            {

                return new Response<Guid>
                {
                    StatusMessage = "Account not found"
                };
            }

            reqModel.PayableCode = getaccount.Description;
            reqModel.Payableid = getaccount.Listid;


            var entity = reqModel.Adapt<Buyer>();

           
            var ss = await Repository.Add((Buyer)(entity as IMinBase ??
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




}