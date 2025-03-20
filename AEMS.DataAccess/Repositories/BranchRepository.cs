using IMS.Domain.Context;
using IMS.Domain.Entities;
using IMS.Domain.Utilities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories;

public interface IBranchRepository
{
    Task<(Pagination, IList<Branch>)> GetAll(Pagination pagination, bool onlyUsers);
}

public class BranchRepository : BaseRepository<Branch>, IBranchRepository
{
    public BranchRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(Pagination, IList<Branch>)> GetAll(Pagination pagination, bool onlyUsers)
    {
        var total = 0;
        var totalPages = 0;
        IList<Branch> res = null;
        if (onlyUsers)
        {
            res = await DbSet
                .Include(x => x.Organization).ThenInclude(u => u.OrganizationUsers)
                .Where(f => f.IsDeleted != true && f.Organization.OrganizationUsers.Any(x => x.UserId == Guid.Parse(pagination.RefId)))
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