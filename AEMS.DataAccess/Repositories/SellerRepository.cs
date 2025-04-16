using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories;

  public interface ISellerRepository
 {

 }

public class SellerRepository : BaseRepository<Seller>, ISellerRepository
{
    public SellerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

   
}