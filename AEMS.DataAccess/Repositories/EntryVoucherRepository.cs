using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;
public interface IEntryVoucherRepository
{

}
public class EntryVoucherRepository : BaseRepository<EntryVoucher>, IEntryVoucherRepository
{
    public EntryVoucherRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}