using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IMS.DataAccess.Repositories
{
    public interface IAssetsRepository
    {
        Task<IList<Assets>> GetByParent(Guid id);
    }

    public class AssetsRepository : BaseRepository<Assets>, IAssetsRepository
    {
        public AssetsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }



        public async Task<IList<Assets>> GetByParent(Guid id)
        {
            return await DbContext.Assets.Where(p => p.Id == id).ToListAsync();
        }
    }
}
