using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories
{
    public interface IBlendRatioRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class BlendRatioRepository : BaseRepository<BlendRatio>, IBlendRatioRepository
    {
        public BlendRatioRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}