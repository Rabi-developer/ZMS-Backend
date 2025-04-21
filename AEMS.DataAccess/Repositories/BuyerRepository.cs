using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories;

  public interface IBuyerRepository
 {

 }

public class BuyerRepository : BaseRepository<Buyer>, IBuyerRepository
{
    public BuyerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

   
}