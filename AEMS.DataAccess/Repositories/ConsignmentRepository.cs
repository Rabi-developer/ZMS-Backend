using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface IConsignmentRepository
{

}

public class ConsignmentRepository : BaseRepository<Consignment>, IConsignmentRepository
{
    public ConsignmentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}