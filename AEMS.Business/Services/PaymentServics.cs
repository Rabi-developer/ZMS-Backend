using IMS.Business.DTOs.Requests;
using ZMS.Business.DTOs.Responses;
using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Context;
using IMS.Domain.Entities;
using IMS.Domain.Utilities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;
using ZMS.Domain.Entities;
using ZMS.DataAccess.Repositories;

namespace IMS.Business.Services
{
    public interface IPaymentService : IBaseService<PaymentReq, PaymentRes, Payment>
    {
        Task<Response<PaymentRes>> GetBySellerBuyer(string seller, string buyer);
        Task<PaymentStatus> UpdateStatusAsync(Guid id, string status);
        Task<Response<List<string>>> GetPaymentNumbers();
        Task<Response<List<string>>> GetChequeNumbers(Guid paymentId);
    }

    public class PaymentService : BaseService<PaymentReq, PaymentRes, PaymentRepository, Payment>, IPaymentService
    {
        private readonly IPaymentRepository _repository;
        private readonly IHttpContextAccessor _context;
        private readonly ApplicationDbContext _DbContext;

        public PaymentService(IUnitOfWork unitOfWork, IHttpContextAccessor context, ApplicationDbContext dbContext) : base(unitOfWork)
        {
            _repository = UnitOfWork.GetRepository<PaymentRepository>();
            _context = context;
            _DbContext = dbContext;
        }

        public override async Task<Response<IList<PaymentRes>>> GetAll(Pagination? paginate)
        {
            try
            {
                var pagination = paginate ?? new Pagination();
                var (pag, data) = await Repository.GetAll(pagination, query => query.Include(p => p.RelatedInvoices));

                return new Response<IList<PaymentRes>>
                {
                    Data = data.Adapt<List<PaymentRes>>(),
                    Misc = pag,
                    StatusMessage = "Fetch successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response<IList<PaymentRes>>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public override async Task<Response<Guid>> Add(PaymentReq reqModel)
        {
            try
            {
                var lastPayment = await _DbContext.Payments
                    .OrderByDescending(x => x.PaymentNumber)
                    .FirstOrDefaultAsync();

                string newPaymentNumber = lastPayment == null
                    ? "1"
                    : (int.Parse(lastPayment.PaymentNumber) + 1).ToString("D1");

                var entity = reqModel.Adapt<Payment>();
                entity.PaymentNumber = newPaymentNumber;
                var relatedInvoices = entity.RelatedInvoices;

                
                foreach (var invoice in relatedInvoices)
                {
                    float receivedAmount;
                    float currentBalance;
                    float adjusted;

                    // Safe parsing
                    if (float.TryParse(invoice.ReceivedAmount, out receivedAmount) &&
                        float.TryParse(invoice.Balance, out currentBalance) &&
                        float.TryParse(invoice.InvoiceAdjusted, out adjusted) )
                    {
                        float newBalance = receivedAmount - currentBalance - adjusted;
                        invoice.Balance = newBalance.ToString("F2"); // Format to 2 decimal places if needed
                        
                    }
                }

                
                await Repository.Add(entity);
                await UnitOfWork.SaveAsync();

                return new Response<Guid>
                {
                    Data = entity.Id,
                    StatusMessage = "Created successfully",
                    StatusCode = HttpStatusCode.Created
                };
            }
            catch (Exception e)
            {
                return new Response<Guid>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public override async Task<Response<PaymentRes>> Get(Guid id)
        {
            try
            {
                var entity = await Repository.Get(id, query => query.Include(p => p.RelatedInvoices));
                if (entity == null)
                {
                    return new Response<PaymentRes>
                    {
                        StatusMessage = $"{typeof(Payment).Name} Not found",
                        StatusCode = HttpStatusCode.NoContent
                    };
                }
                return new Response<PaymentRes>
                {
                    Data = entity.Adapt<PaymentRes>(),
                    StatusMessage = "Fetch successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response<PaymentRes>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<PaymentRes>> GetBySellerBuyer(string seller, string buyer)
        {
            try
            {
                var getdata = await _DbContext.Payments
                    .Where(p => p.Seller == seller && p.Buyer == buyer && p.IsDeleted != true)
                    .Include(p => p.RelatedInvoices)
                    .OrderByDescending(p => p.Id)
                    .FirstOrDefaultAsync();

                if (getdata == null)
                {
                    return new Response<PaymentRes>
                    {
                        StatusMessage = "Not Found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                return new Response<PaymentRes>
                {
                    Data = getdata.Adapt<PaymentRes>(),
                    StatusMessage = "Fetch successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response<PaymentRes>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public override async Task<Response<PaymentRes>> Update(PaymentReq reqModel)
        {
            try
            {
                var entity = reqModel.Adapt<Payment>();
                var existingEntity = await Repository.Get(entity.Id, query => query.Include(p => p.RelatedInvoices));
                if (existingEntity == null)
                {
                    return new Response<PaymentRes>
                    {
                        StatusMessage = $"{typeof(Payment).Name} not found with Id: {entity.Id}",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                _DbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
                existingEntity.RelatedInvoices ??= new List<RelatedInvoice>();

                var existingInvoiceIds = existingEntity.RelatedInvoices.Select(ri => ri.Id).ToList();
                var incomingInvoiceIds = entity.RelatedInvoices?.Select(ri => ri.Id).Where(id => id.HasValue).Select(id => id.Value).ToList() ?? new List<Guid>();

                existingEntity.RelatedInvoices.RemoveAll(ri => !incomingInvoiceIds.Contains(ri.Id.Value));

                if (entity.RelatedInvoices != null)
                {
                    foreach (var incomingInvoice in entity.RelatedInvoices.Where(ri => ri.Id.HasValue))
                    {
                        var existingInvoice = existingEntity.RelatedInvoices.FirstOrDefault(ri => ri.Id == incomingInvoice.Id);
                        if (existingInvoice == null)
                        {
                            var newInvoice = incomingInvoice.Adapt<RelatedInvoice>();
                            _DbContext.Entry(newInvoice).State = EntityState.Detached;
                            existingEntity.RelatedInvoices.Add(newInvoice);
                        }
                        else
                        {
                            _DbContext.Entry(existingInvoice).CurrentValues.SetValues(incomingInvoice);
                        }
                    }
                }

                existingEntity.ModifiedDateTime = DateTime.UtcNow;
                await UnitOfWork.SaveAsync();

                return new Response<PaymentRes>
                {
                    Data = existingEntity.Adapt<PaymentRes>(),
                    StatusMessage = "Updated successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response<PaymentRes>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<PaymentStatus> UpdateStatusAsync(Guid id, string status)
        {
            if (status == null || id == null)
            {
                throw new ArgumentException("Payment ID and Status are required.");
            }

            var validStatuses = new[] { "Pending", "Approved", "Canceled", "Completed" };
            if (!validStatuses.Contains(status))
            {
                throw new ArgumentException($"Status must be one of: {string.Join(", ", validStatuses)}");
            }

            var payment = await _DbContext.Payments.Where(p => p.Id == id).FirstOrDefaultAsync();

            if (payment == null)
            {
                throw new KeyNotFoundException($"Payment with ID {id} not found.");
            }

            payment.Status = status;
            payment.UpdatedBy = _context.HttpContext?.User.Identity?.Name ?? "System";
            payment.UpdationDate = DateTime.UtcNow.ToString("o");

            await UnitOfWork.SaveAsync();

            return new PaymentStatus
            {
                Id = id,
                Status = status
            };
        }

        public async Task<Response<List<string>>> GetPaymentNumbers()
        {
            try
            {
                var paymentNumbers = await _DbContext.Payments
                    .Where(p => p.IsDeleted != true)
                    .Select(p => p.PaymentNumber)
                    .ToListAsync();

                return new Response<List<string>>
                {
                    Data = paymentNumbers,
                    StatusMessage = "Payment numbers fetched successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response<List<string>>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<List<string>>> GetChequeNumbers(Guid paymentId)
        {
            try
            {
                var chequeNumbers = await _DbContext.Payments
                    .Where(p => p.Id == paymentId && p.IsDeleted != true && !string.IsNullOrEmpty(p.ChequeNo))
                    .Select(p => p.ChequeNo)
                    .ToListAsync();

                return new Response<List<string>>
                {
                    Data = chequeNumbers,
                    StatusMessage = "Cheque numbers fetched successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new Response<List<string>>
                {
                    StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}