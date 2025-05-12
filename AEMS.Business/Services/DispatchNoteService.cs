using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using ZMS.Domain.Entities;

namespace IMS.Business.Services;

public interface IDispatchNoteService : IBaseService<DispatchNoteReq, DispatchNoteRes, DispatchNote>
{
}

public class DispatchNoteService : BaseService<DispatchNoteReq, DispatchNoteRes, DispatchNoteRepository, DispatchNote>, IDispatchNoteService
{
    private readonly IDispatchNoteRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public DispatchNoteService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<DispatchNoteRepository>();
        _context = context;
        _DbContext = dbContextn;
    }

   


}