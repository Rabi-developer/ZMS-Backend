using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IMS.DataAccess.Repositories
{
    public interface IAblRevenueRepository
    {
        Task<IList<AblRevenue>> GetByParent(Guid id);
    }

    public class AblRevenueRepository : BaseRepository<AblRevenue>, IAblRevenueRepository
    {
        public AblRevenueRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }



        public async Task<IList<AblRevenue>> GetByParent(Guid id)
        {
            return await DbContext.AblRevenue.Where(p => p.Id == id).ToListAsync();
        }
    }
}
