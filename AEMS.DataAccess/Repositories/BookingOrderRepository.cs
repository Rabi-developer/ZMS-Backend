using IMS.Domain.Context;
using IMS.Domain.Entities;
using IMS.Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using ZMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;
public interface IBookingOrderRepository
{
    Task GetByBookingOrderIdAsync(Guid bookingOrderId, Pagination? paginate);
    Task Update(object entity);
}
public class BookingOrderRepository : BaseRepository<BookingOrder>, IBookingOrderRepository
{
    public BookingOrderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task GetByBookingOrderIdAsync(Guid bookingOrderId, Pagination? paginate)
    {
        throw new NotImplementedException();
    }

    public Task Update(object entity)
    {
        throw new NotImplementedException();
    }
}