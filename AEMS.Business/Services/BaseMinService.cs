using IMS.DataAccess.UnitOfWork;

namespace IMS.Business.Services;

public interface IBaseMinService
{

}
public class BaseMinService<TRepository> : IBaseMinService where TRepository : class
{
    protected readonly IUnitOfWork UnitOfWork;
    protected readonly TRepository Repository;
    //protected readonly HttpContext Context;

    public BaseMinService(IUnitOfWork unitOfWork)//, IHttpContextAccessor httpContextAccessor)
    {
        UnitOfWork = unitOfWork;

        Repository = unitOfWork.GetRepository<TRepository>();
        //Context = httpContextAccessor.HttpContext ??
        //          throw new NullReferenceException(
        //              "Http Context is Null. Something is Wrongly Configured or Accessed!");
    }
}
