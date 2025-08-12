using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface IMunshyanaRepository
{

}

public class MunshyanaRepository : BaseRepository<Munshyana>, IMunshyanaRepository
{
    public MunshyanaRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}