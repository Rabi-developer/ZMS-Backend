using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories
{
    public interface IStuffRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class StuffRepository : BaseRepository<Stuff>, IStuffRepository
    {
        public StuffRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}