using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IMS.DataAccess.Repositories
{
    public interface ILiabilitiesRepository
    {
        Task<IList<Liabilities>> GetByParent(Guid id);
    }

    public class LiabilitiesRepository : BaseRepository<Liabilities>, ILiabilitiesRepository
    {
        public LiabilitiesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }



        public async Task<IList<Liabilities>> GetByParent(Guid id)
        {
            return await DbContext.Liabilities.Where(p => p.Id == id).ToListAsync();
        }
    }
}
