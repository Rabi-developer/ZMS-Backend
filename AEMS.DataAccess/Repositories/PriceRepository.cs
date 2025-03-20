using IMS.Domain.Context;
using IMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface IPriceRepository
{
}

public class PriceRepository : BaseRepository<Price>, IPriceRepository
{
    public PriceRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

}