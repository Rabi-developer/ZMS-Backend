using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IMS.DataAccess.Repositories
{
    public interface IRevenueRepository
    {
        Task<IList<Revenue>> GetByParent(Guid id);
    }

    public class RevenueRepository : BaseRepository<Revenue>, IRevenueRepository
    {
        public RevenueRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }



        public async Task<IList<Revenue>> GetByParent(Guid id)
        {
            return await DbContext.Revenues.Where(p => p.Id == id).ToListAsync();

        }
    }
}
