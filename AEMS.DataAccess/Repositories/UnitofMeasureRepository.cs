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
    public interface IUnitofMeasureRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class UnitofMeasureRepository : BaseRepository<UnitofMeasure>, IUnitofMeasureRepository
    {
        public UnitofMeasureRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<UnitofMeasure> GetAll()
        {
            return DbSet.AsQueryable();
        }
    }
}
