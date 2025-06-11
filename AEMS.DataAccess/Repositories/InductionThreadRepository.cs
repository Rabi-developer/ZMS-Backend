using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories
{
    public interface IInductionThreadRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class InductionThreadRepository : BaseRepository<InductionThread>, IInductionThreadRepository
    {
        public InductionThreadRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}