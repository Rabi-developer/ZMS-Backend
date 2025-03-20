using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;
using IMS.Domain.Utilities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace IMS.Business.Services;

public interface IProductService : IBaseService<ProductReq, ProductRes, Product>
{
    
}
public class ProductService : BaseService<ProductReq, ProductRes, ProductRepository, Product>, IProductService
{


    private readonly IProductRepository _repository;
    private readonly IHttpContextAccessor _context;
    public ProductService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<ProductRepository>();
        _context = context;
    }

    public async override Task<Response<IList<ProductRes>>> GetAll(Pagination? paginate)
    {
        try
        {
            var pagination = paginate ?? new Pagination();
            // TODO: Get Pagination from the Query

            var (pag, data) = await Repository.GetAll(pagination,
                query => query.Include(order => order.Brand)
                  .Include(order => order.Unit).Include(order => order.Category));
            TypeAdapterConfig<Product, ProductRes>.NewConfig()
     .Map(dest => dest.Unit, src => src.Unit.Name)
     .Map(dest => dest.Brand, src => src.Brand.Name)
     .Map(dest => dest.Category, src => src.Category.Name);
            return new Response<IList<ProductRes>>
            {
                Data = data.Adapt<IList<ProductRes>>(),
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<ProductRes>>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }




}