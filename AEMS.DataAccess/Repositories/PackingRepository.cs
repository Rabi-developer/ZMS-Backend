using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories
{
    public interface IPackingRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class PackingRepository : BaseRepository<Packing>, IPackingRepository
    {
        public PackingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Packing> GetAll()
        {
            return DbSet.AsQueryable();
        }
    }
}