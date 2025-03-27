using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.DataAccess.Repositories
{
    public interface ICapitalAccountRepository
    {
        Task<IList<CapitalAccount>> GetByParent(Guid parentId);
        Task<IList<CapitalAccount>> GetAllHierarchy();
    }

    public class CapitalAccountRepository : BaseRepository<CapitalAccount>, ICapitalAccountRepository
    {
        public CapitalAccountRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IList<CapitalAccount>> GetAllHierarchy()
        {
            var allAccounts = await DbContext.CapitalAccounts
                .Include(a => a.Children)
                .AsNoTracking()
                .ToListAsync();

            var lookup = allAccounts.ToLookup(a => a.ParentAccountId);

            foreach (var account in allAccounts)
            {
                account.Children = lookup[account.Id]
                    .OrderBy(a => a.Listid)
                    .ToList();
            }

            return lookup[null]
                .OrderBy(a => a.Listid)
                .ToList();
        }

        public async Task<IList<CapitalAccount>> GetByParent(Guid parentId)
        {
            return await DbContext.CapitalAccounts
                .Where(p => p.ParentAccountId == parentId)
                .OrderBy(p => p.Listid)
                .ToListAsync();
        }
    }
}