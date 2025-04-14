using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories
{
    public interface IWeftYarnTypeRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class WeftYarnTypeRepository : BaseRepository<WeftYarnType>, IWeftYarnTypeRepository
    {
        public WeftYarnTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}