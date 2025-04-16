using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories
{
    public interface IPeicelengthRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class PeicelengthRepository : BaseRepository<Peicelength>, IPeicelengthRepository
    {
        public PeicelengthRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Peicelength> GetAll()
        {
            return DbSet.AsQueryable();
        }
    }
}