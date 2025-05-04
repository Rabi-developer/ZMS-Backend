using IMS.Business.DTOs.Requests;
using IMS.Business.Services;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Entities;

public interface IGeneralSaleTextTypeService : IBaseService<GeneralSaleTextTypeReq, GeneralSaleTextTypeRes, GeneralSaleTextType>
{

}
public class GeneralSaleTextTypeService : BaseService<GeneralSaleTextTypeReq, GeneralSaleTextTypeRes, GeneralSaleTextTypeRepository, GeneralSaleTextType>, IGeneralSaleTextTypeService
{
    public GeneralSaleTextTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}