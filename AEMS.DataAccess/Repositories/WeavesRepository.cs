using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories
{
    public interface IWeavesRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class WeavesRepository : BaseRepository<Weaves>, IWeavesRepository
    {
        public WeavesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}