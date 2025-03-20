using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IMS.DataAccess.Repositories
{
    public interface IEmployeeRepository
    {

    }
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Employee?> Get(Guid id)
        {
            return await DbSet
                .Include(e => e.Address) 
                .FirstOrDefaultAsync(e => !e.IsDeleted && e.Id == id);
        }
    }
}