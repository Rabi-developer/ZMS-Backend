using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IMS.DataAccess.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> Get(Guid id); // Get a customer by ID
    }

    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Customer?> Get(Guid id)
        {
            return await DbSet
                .Include(c => c.AccountNumbers) // Include related account numbers
                .FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == id); // Filter by ID and ensure it's not deleted
        }
    }
}