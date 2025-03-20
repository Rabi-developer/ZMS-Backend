using IMS.Domain.Context;
using IMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface IAddressRepository
{
}

public class AddressRepository : BaseRepository<Address>, IAddressRepository
{
    public AddressRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

}