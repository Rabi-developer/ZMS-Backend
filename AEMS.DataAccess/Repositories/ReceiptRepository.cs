using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface IReceiptRepository
{

}

public class ReceiptRepository : BaseRepository<Receipt>, IReceiptRepository
{
    public ReceiptRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}