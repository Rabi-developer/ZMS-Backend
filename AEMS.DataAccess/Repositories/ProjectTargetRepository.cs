using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IMS.DataAccess.Repositories
{
    public interface IProjectTargetRepository
    {
        Task<ProjectTarget?> Get(Guid id);
    }
    public class ProjectTargetRepository : BaseRepository<ProjectTarget>, IProjectTargetRepository
    {
        public ProjectTargetRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ProjectTarget?> Get(Guid id)
        {
            return await DbSet
                .Include(pt => pt.Employee) 
                .FirstOrDefaultAsync(pt => !pt.IsDeleted && pt.Id == id);
        }
    }
}