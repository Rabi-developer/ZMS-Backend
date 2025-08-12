using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface IPartyRepository
{

}

public class PartyRepository : BaseRepository<Party>, ICommisionInvoiceRepository
{
    public PartyRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}