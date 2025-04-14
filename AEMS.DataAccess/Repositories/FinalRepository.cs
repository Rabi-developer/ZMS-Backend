using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories
{
    public interface IFinalRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class FinalRepository : BaseRepository<Final>, IFinalRepository
    {
        public FinalRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}