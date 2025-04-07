using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories
{
    public interface IDescriptionRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class DescriptionRepository : BaseRepository<Description>, IDescriptionRepository
    {
        public DescriptionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}