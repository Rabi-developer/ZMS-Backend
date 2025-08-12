using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface IBusinessAssociateRepository
{

}

public class BusinessAssociateRepository : BaseRepository<BusinessAssociate>, IBusinessAssociateRepository
{
    public BusinessAssociateRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}