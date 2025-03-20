using IMS.Domain.Context;
using IMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface ILevelRepository
{
}

public class LevelRepository : BaseRepository<Level>, ILevelRepository
{
    public LevelRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

}