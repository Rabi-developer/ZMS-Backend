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
    public interface IDeliveryTermRepository
    {
        // No additional methods needed for basic CRUD
    }

    public class DeliveryTermRepository : BaseRepository<DeliveryTerm>, IDeliveryTermRepository
    {
        public DeliveryTermRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<DeliveryTerm> GetAll()
        {
            return DbSet.AsQueryable();
        }
    }
}
