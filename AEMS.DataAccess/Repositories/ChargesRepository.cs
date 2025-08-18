using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using ZMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface IChargesRepository
{
}

public class ChargesRepository : BaseRepository<Charges>, IChargesRepository
{
    public ChargesRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}