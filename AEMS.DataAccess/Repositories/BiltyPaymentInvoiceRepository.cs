using IMS.Domain.Context;
using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using ZMS.Domain.Entities;

namespace IMS.DataAccess.Repositories;

public interface IBiltyPaymentInvoiceRepository
{
}

public class BiltyPaymentInvoiceRepository : BaseRepository<BiltyPaymentInvoice>, IBiltyPaymentInvoiceRepository
{
    public BiltyPaymentInvoiceRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}