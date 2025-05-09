using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface IContractRepository
{
    
}

public class ContractRepository : BaseRepository<Contract>, IContractRepository
{
    public ContractRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}