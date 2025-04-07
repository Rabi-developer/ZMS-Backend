using IMS.Domain.Context;
using IMS.Domain.Entities;
using IMS.Domain.Migrations.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories
{
    public interface IWrapYarnTypeRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class WrapYarnTypeRepository : BaseRepository<WrapYarnType>, IWrapYarnTypeRepository
    {
        public WrapYarnTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}