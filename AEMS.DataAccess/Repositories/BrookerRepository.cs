using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface IBrookerRepository
{

}

public class BrookerRepository : BaseRepository<Brooker>, IBrookerRepository
{
    public BrookerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}