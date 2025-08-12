using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface ICommisionInvoiceRepository
{

}

public class InspectionNoteRepository : BaseRepository<InspectionNote>, ICommisionInvoiceRepository
{
    public InspectionNoteRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}