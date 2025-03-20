using IMS.Domain.Context;
using IMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface IStockRepository
{

}

public class StockRepository : BaseRepository<Stock>, IStockRepository
{
    public StockRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
