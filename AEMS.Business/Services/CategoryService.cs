using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace IMS.Business.Services;

public interface ICategoryService : IBaseService<CategoryReq, CategoryRes, Category>
{

}
public class CategoryService : BaseService<CategoryReq, CategoryRes, CategoryRepository, Category>, ICategoryService
{


    private readonly ICategoryRepository _repository;
    private readonly IHttpContextAccessor _context;
    public CategoryService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork)
    {
        _repository = UnitOfWork.GetRepository<CategoryRepository>();
        _context = context;
    }


}