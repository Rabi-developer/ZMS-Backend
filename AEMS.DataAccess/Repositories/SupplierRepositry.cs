using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IMS.DataAccess.Repositories
{
    public interface ISupplierRepository
    {
        Task<Supplier?> Get(Guid id);
    }
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Supplier?> Get(Guid id)
        {
            return await DbSet
                .Include(s => s.AccountNumbers) 
                .FirstOrDefaultAsync(s => !s.IsDeleted && s.Id == id);
        }
    }
}