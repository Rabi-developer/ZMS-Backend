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

public interface IContractService : IBaseService<ContractReq, ContractRes, Contract>
{
   public Task<ContractStatus> UpdateStatusAsync(Guid id, string status);
}

public class ContractService : BaseService<ContractReq, ContractRes, ContractRepository, Contract>, IContractService
{
    private readonly IContractRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly ApplicationDbContext _DbContext;

    public ContractService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContextn) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<ContractRepository>();
        _context = context;
        _DbContext = dbContextn;
    }

    public async Task<ContractStatus> UpdateStatusAsync(Guid id, string status)
    {
        if (status == null || id == null )
        {
            throw new ArgumentException("Contract ID and Status are required.");
        }

        var validStatuses = new[] { "Pending", "Approved", "Canceled", "Closed Dispatch", "Closed Payment", "Complete Closed" };
        if (!validStatuses.Contains(status))
        {
            throw new ArgumentException($"Status must be one of: {string.Join(", ", validStatuses)}");
        }

        var contract = await _DbContext.contracts.Where(p => p.Id == id).FirstOrDefaultAsync();

       



        if (contract == null)
        {
            throw new KeyNotFoundException($"Contract with ID {id} not found.");
        }

        contract.Status = status;
        contract.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
        contract.UpdationDate = DateTime.UtcNow.ToString("o");

        await UnitOfWork.SaveAsync();

        return new ContractStatus 
        {
            Id = id,
            Status = status,
        };
    }

    
    
}