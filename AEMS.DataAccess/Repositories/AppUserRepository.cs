using IMS.Domain.Context;

namespace IMS.DataAccess.Repositories;

public interface IAppUserRepository
{
}

public class AppUserRepository : IAppUserRepository
{
    protected readonly ApplicationDbContext _DbContext;

    public AppUserRepository(ApplicationDbContext dbContext)
    {
        _DbContext = dbContext;

    }

}