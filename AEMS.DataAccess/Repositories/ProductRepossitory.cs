using IMS.Domain.Context;
using IMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface IProductRepository
{
}

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

}