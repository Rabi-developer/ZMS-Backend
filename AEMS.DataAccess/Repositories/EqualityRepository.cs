using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IMS.DataAccess.Repositories
{
    public interface IEqualityRepository
    {
        Task<IList<Equality>> GetByParent(Guid id);
    }

    public class EqualityRepository : BaseRepository<Equality>, IEqualityRepository
    {
        public EqualityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }



        public async Task<IList<Equality>> GetByParent(Guid id)
        {
            return await DbContext.Equality.Where(p => p.Id == id).ToListAsync();
        }
    }
}
