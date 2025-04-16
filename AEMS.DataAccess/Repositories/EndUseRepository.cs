using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories
{
    public interface IEndUseRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class EndUseRepository : BaseRepository<EndUse>, IEndUseRepository
    {
        public EndUseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<EndUse> GetAll()
        {
            return DbSet.AsQueryable();
        }
    }
}