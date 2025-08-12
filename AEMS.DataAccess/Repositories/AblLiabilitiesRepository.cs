using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IMS.DataAccess.Repositories
{
    public interface IAblLiabilitiesRepository
    {
        Task<IList<AblLiabilities>> GetByParent(Guid id);
    }

    public class AblLiabilitiesRepository : BaseRepository<AblLiabilities>, IAblLiabilitiesRepository
    {
        public AblLiabilitiesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }



        public async Task<IList<AblLiabilities>> GetByParent(Guid id)
        {
            return await DbContext.AblLiabilities.Where(p => p.Id == id).ToListAsync();
        }
    }
}
