using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories
{
    public interface IPickInsertionRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class PickInsertionRepository : BaseRepository<PickInsertion>, IPickInsertionRepository
    {
        public PickInsertionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}