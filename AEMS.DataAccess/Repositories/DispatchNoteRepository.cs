using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface IDispatchNoteRepository
{

}

public class DispatchNoteRepository : BaseRepository<DispatchNote>, IDispatchNoteRepository
{
    public DispatchNoteRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<DispatchNote> GetAll()
    {
        return DbSet.AsQueryable();
    }
}