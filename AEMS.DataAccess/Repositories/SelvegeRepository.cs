using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories
{
    public interface ISelvegeRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class SelvegeRepository : BaseRepository<Selvege>, ISelvegeRepository
    {
        public SelvegeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Selvege> GetAll()
        {
            return DbSet.AsQueryable();
        }
    }
}