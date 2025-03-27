using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IMS.DataAccess.Repositories
{
    public interface IExpenseRepository
    {
        Task<IList<Expense>> GetByParent(Guid id);
    }

    public class ExpenseRepository : BaseRepository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }



        public async Task<IList<Expense>> GetByParent(Guid id)
        {
            return await DbContext.Expenses.Where(p => p.Id == id).ToListAsync();

        }
    }
}
