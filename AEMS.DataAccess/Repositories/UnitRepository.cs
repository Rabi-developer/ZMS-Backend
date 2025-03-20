using IMS.Domain.Context;
using IMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface IUnitRepository
{
}

public class UnitRepository : BaseRepository<Unit>, IUnitRepository
{
    public UnitRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

}