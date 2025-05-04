using IMS.DataAccess.Repositories;
using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMS.Domain.Entities;

namespace ZMS.DataAccess.Repositories
{
    public interface ICommisionTypeRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class CommisionTypeRepository : BaseRepository<CommisionType>, ICommisionTypeRepository
    {
        public CommisionTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<CommisionType> GetAll()
        {
            return DbSet.AsQueryable();
        }
    }
}
