using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.DataAccess.Repositories
{
    public interface IAccountIdRepository
    {
       
        Task<IList<AccountId>> GetByParent(Guid parentId);
        Task<IList<AccountId>> GetAllHierarchy();
    }

    public class AccountIdRepository : BaseRepository<AccountId>, IAccountIdRepository
    
    {
        private readonly ApplicationDbContext _context;

        public AccountIdRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

       

        public async Task<IList<AccountId>> GetAllHierarchy()
        {
            var allAccounts = await _context.AccountIds
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
            return lookup[null].OrderBy(a => a.Listid).ToList();
        }

        public async Task<IList<AccountId>> GetByParent(Guid parentId)
        {
            return await _context.AccountIds
                .Where(p => p.ParentAccountId == parentId)
                .OrderBy(p => p.Listid)
                .ToListAsync();
        }
    }
}