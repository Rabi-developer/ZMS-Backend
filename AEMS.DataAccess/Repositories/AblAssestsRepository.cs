using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IMS.DataAccess.Repositories
{
    public interface IAblAssestsRepository
    {
        Task<IList<AblAssests>> GetByParent(Guid id);
    }

    public class AblAssestsRepository : BaseRepository<AblAssests>, IAblAssestsRepository
    {
        public AblAssestsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }



        public async Task<IList<AblAssests>> GetByParent(Guid id)
        {
            return await DbContext.AblAssests.Where(p => p.Id == id).ToListAsync();
        }
    }
}
