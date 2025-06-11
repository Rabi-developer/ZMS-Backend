using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories
{
    public interface IGsmRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class GsmRepository : BaseRepository<Gsm>, IGsmRepository
    {
        public GsmRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}