using IMS.Domain.Context;
using IMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface IStockMovementRepository
{
}

public class StockMovementRepository : BaseRepository<StockMovement>, IStockMovementRepository
{
    public StockMovementRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
