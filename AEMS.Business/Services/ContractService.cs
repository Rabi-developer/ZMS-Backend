using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Context;
using IMS.Domain.Entities;
using IMS.Domain.Utilities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
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
    public async override Task<Response<IList<ContractRes>>> GetAll(Pagination? paginate)
    {
        try
        {
            var pagination = paginate ?? new Pagination();

            // Fetch paged data
            var (pag, data) = await Repository.GetAll(pagination, null);

            // Fetch all lookup tables
            var fabricTypes = await _DbContext.FabricTypes.ToListAsync();
            var stuffTypes = await _DbContext.Stuffs.ToListAsync();
            var blendRatios = await _DbContext.BlendRatio.ToListAsync();
            var final = await _DbContext.Finals.ToListAsync();
            var Description = await _DbContext.Descriptions.ToListAsync();
            var warpYarnTypes = await _DbContext.WrapYarnTypes.ToListAsync();
            var weftYarnTypes = await _DbContext.WeftYarnTypes.ToListAsync();
            var weavesList = await _DbContext.Weaves.ToListAsync();
            var pickInsertions = await _DbContext.PickInsertion.ToListAsync();
            var packings = await _DbContext.Packings.ToListAsync();
            var pieceLengths = await _DbContext.Peicelengths.ToListAsync();
            var endUses = await _DbContext.EndUses.ToListAsync();
            var gstTypes = await _DbContext.GeneralSaleTexts.ToListAsync();
            var selvege = await _DbContext.Selveges.ToListAsync();
            var selvegeWeaves = await _DbContext.SelvegeWeaves.ToListAsync();
            var selvegeWidth = await _DbContext.SelvegeWidths.ToListAsync();
            var peicelengths = await _DbContext.Peicelengths.ToListAsync();
            var seller = await _DbContext.Sellers.ToListAsync();
            var Buyer = await _DbContext.Buyers.ToListAsync();



            // Map to DTO
            var result = data.Adapt<List<ContractRes>>();

            // Replace listid values with Descriptions
            // Replace listid values with Descriptions
            foreach (var item in result)
            {
                if (!string.IsNullOrWhiteSpace(item.FabricType))
                    item.FabricType = fabricTypes.FirstOrDefault(f => f.Listid == item.FabricType)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.Stuff))
                    item.Stuff = stuffTypes.FirstOrDefault(s => s.Listid == item.Stuff)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.Selvege))
                    item.Selvege = selvege.FirstOrDefault(s => s.Listid == item.Selvege)?.Descriptions;
                
                if (!string.IsNullOrWhiteSpace(item.SelvegeWeaves))
                    item.SelvegeWeaves = selvegeWeaves.FirstOrDefault(s => s.Listid == item.SelvegeWeaves)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.SelvegeWidth))
                    item.SelvegeWidth = selvegeWidth.FirstOrDefault(s => s.Listid == item.SelvegeWidth)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.BlendRatio))
                    item.BlendRatio = blendRatios.FirstOrDefault(b => b.Listid == item.BlendRatio)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.Final))
                    item.Final = final.FirstOrDefault(w => w.Listid == item.Final)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.WarpYarnType))
                    item.WarpYarnType = warpYarnTypes.FirstOrDefault(w => w.Listid == item.WarpYarnType)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.WeftYarnType))
                    item.WeftYarnType = weftYarnTypes.FirstOrDefault(w => w.Listid == item.WeftYarnType)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.Weaves))
                    item.Weaves = weavesList.FirstOrDefault(w => w.Listid == item.Weaves)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.PickInsertion))
                    item.PickInsertion = pickInsertions.FirstOrDefault(p => p.Listid == item.PickInsertion)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.Packing))
                    item.Packing = packings.FirstOrDefault(p => p.Listid == item.Packing)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.PieceLength))
                    item.PieceLength = pieceLengths.FirstOrDefault(p => p.Listid == item.PieceLength)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.EndUse))
                    item.EndUse = endUses.FirstOrDefault(e => e.Listid == item.EndUse)?.Descriptions;

                if (!string.IsNullOrWhiteSpace(item.Gst))
                    item.Gst = gstTypes.FirstOrDefault(g => g.GstType == item.Gst)?.GstType;

                // Fixed property names and consistency
}

            return new Response<IList<ContractRes>>
            {
                Data = result,
                Misc = pag,
                StatusMessage = "Fetched successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<ContractRes>>
            {
                StatusMessage = e.InnerException?.Message ?? e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
    public async Task<ContractStatus> UpdateStatusAsync(Guid id, string status)
    {
        if (status == null || id == null)
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