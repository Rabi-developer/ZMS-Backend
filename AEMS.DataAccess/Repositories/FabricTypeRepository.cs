using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories
{
    public interface IFabricTypeRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class FabricTypeRepository : BaseRepository<FabricType>, IFabricTypeRepository
    {
        public FabricTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<FabricType> GetAll()
        {
            return DbSet.AsQueryable();
        }
    }
}