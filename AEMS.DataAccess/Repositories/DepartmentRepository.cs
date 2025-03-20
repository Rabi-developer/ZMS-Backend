using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories;

  public interface IDepartmentRepository
 {

 }

public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
{
    public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Department?> Get(Guid id)
    {
        return await 
        DbSet.Include(x => x.Address).Include(x => x.Branch).FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == id);
    }
}