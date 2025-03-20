using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IMS.DataAccess.Repositories
{
    public interface ICapitalAccountRepository
    {
        Task<IList<CapitalAccount>> GetByParent(Guid id);
    }

        public class CapitalAccountRepository : BaseRepository<CapitalAccount>, ICapitalAccountRepository
        {
            public CapitalAccountRepository(ApplicationDbContext dbContext) : base(dbContext)
            {
            }

           

        public async Task<IList<CapitalAccount>> GetByParent(Guid id)
        {
            return await DbContext.CapitalAccounts.Where(p => p.Id == id).ToListAsync();
        }
    }
    }
