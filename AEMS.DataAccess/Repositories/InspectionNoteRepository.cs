using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface IInspectionNoteRepository
{

}

public class InspectionNoteRepository : BaseRepository<InspectionNote>, IInspectionNoteRepository
{
    public InspectionNoteRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}