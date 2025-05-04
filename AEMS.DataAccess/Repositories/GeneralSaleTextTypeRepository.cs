using IMS.Domain.Context;
using IMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface IGeneralSaleTextTypeRepository
{
}

public class GeneralSaleTextTypeRepository : BaseRepository<GeneralSaleTextType>, IGeneralSaleTextTypeRepository
{
    public GeneralSaleTextTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

}