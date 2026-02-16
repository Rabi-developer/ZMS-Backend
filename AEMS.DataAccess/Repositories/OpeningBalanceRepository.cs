using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;
public interface IOpeningBalanceRepository
{

}
public class OpeningBalanceRepository : BaseRepository<OpeningBalance>, IOpeningBalanceRepository
{
    public OpeningBalanceRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}