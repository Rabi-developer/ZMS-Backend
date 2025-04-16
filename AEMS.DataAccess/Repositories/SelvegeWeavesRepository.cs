using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories
{
    public interface ISelvegeWeavesRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class SelvegeWeavesRepository : BaseRepository<SelvegeWeaves>, ISelvegeWeavesRepository
    {
        public SelvegeWeavesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<SelvegeWeaves> GetAll()
        {
            return DbSet.AsQueryable();
        }
    }
}