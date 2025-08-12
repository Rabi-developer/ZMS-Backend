using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface ITransporterRepository
{

}

public class TransporterRepository : BaseRepository<Transporter>, ITransporterRepository
{
    public TransporterRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}