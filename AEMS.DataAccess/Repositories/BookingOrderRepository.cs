using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;
public interface IBookingOrderRepository
{

}
public class BookingOrderRepository : BaseRepository<BookingOrder>, IBookingOrderRepository
{
    public BookingOrderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}