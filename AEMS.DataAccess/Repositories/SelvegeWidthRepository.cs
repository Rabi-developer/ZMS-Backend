using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories
{
    public interface ISelvegeWidthRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class SelvegeWidthRepository : BaseRepository<SelvegeWidth>, ISelvegeWidthRepository
    {
        public SelvegeWidthRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<SelvegeWidth> GetAll()
        {
            return DbSet.AsQueryable();
        }
    }
}