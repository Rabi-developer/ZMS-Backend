using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories
{
    public interface ISelvegeThicknessRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class SelvegeThicknessRepository : BaseRepository<SelvegeThickness>, ISelvegeThicknessRepository
    {
        public SelvegeThicknessRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}