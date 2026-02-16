using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;
public interface IAccountOpeningBalanceRepository
{

}
public class AccountOpeningBalanceRepository : BaseRepository<AccountOpeningBalance>, IAccountOpeningBalanceRepository
{
    public AccountOpeningBalanceRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}