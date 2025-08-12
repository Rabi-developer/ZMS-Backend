using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface IVendorRepository
{

}

public class VendorRepository : BaseRepository<Vendor>, IVendorRepository
{
    public VendorRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}