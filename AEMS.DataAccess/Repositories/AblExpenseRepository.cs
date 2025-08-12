using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IMS.DataAccess.Repositories
{
    public interface IAblExpenseRepository
    {
        Task<IList<AblExpense>> GetByParent(Guid id);
    }

    public class AblExpenseRepository : BaseRepository<AblExpense>, IAblExpenseRepository
    {
        public AblExpenseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }



        public async Task<IList<AblExpense>> GetByParent(Guid id)
        {
            return await DbContext.AblExpense.Where(p => p.Id == id).ToListAsync();
        }
    }
}
