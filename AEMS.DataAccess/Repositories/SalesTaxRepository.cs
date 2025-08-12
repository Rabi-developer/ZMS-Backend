using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface ISalesTaxRepository
{

}

public class SalesTaxRepository : BaseRepository<SalesTax>, ISalesTaxRepository
{
    public SalesTaxRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}