using IMS.Domain.Context;
using IMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;


public interface IStockReorderRepository
{

}

    public class StockReorderRepository : BaseRepository<StockReorder>, IStockReorderRepository
{
        public StockReorderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        
    }

