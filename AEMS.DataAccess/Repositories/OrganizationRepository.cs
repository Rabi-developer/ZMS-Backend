using IMS.Domain.Context;
using IMS.Domain.Entities;
using IMS.Domain.Utilities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories;

public interface IOrganizationRepository
{
    Task<(Pagination, IList<Organization>)> GetAll(Pagination pagination, bool onlyUsers);
}

public class OrganizationRepository : BaseRepository<Organization>, IOrganizationRepository
{
    public OrganizationRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(Pagination, IList<Organization>)> GetAll(Pagination pagination, bool onlyUsers)
    {
        var total = 0;
        var totalPages = 0;
        IList<Organization> res = null;
        if (onlyUsers)
        {
            res = await DbSet.Include(x => x.OrganizationUsers).Where(f => f.IsDeleted != true && f.OrganizationUsers.Any(x => x.UserId == Guid.Parse(pagination.RefId)))
            .Paginate((int)pagination.PageIndex, (int)pagination.PageSize, ref total, ref totalPages).ToListAsync();
        }
        else
        {
            res = await DbSet.Where(f => f.IsDeleted != true)
            .Paginate((int)pagination.PageIndex, (int)pagination.PageSize, ref total, ref totalPages).ToListAsync();
        }
        pagination = pagination.Combine(total, totalPages);
        return (pagination, res);
    }

}