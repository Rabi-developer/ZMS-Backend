using IMS.Business.Utitlity;
using IMS.DataAccess.Repositories;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Base;
using IMS.Domain.Utilities;
using IMS.DataAccess.UnitOfWork;
using IMS.Domain.Base;
using IMS.Domain.Utilities;
using Mapster;
using System.Net;

namespace IMS.Business.Services;

public interface IBaseService<in TDto, TResp, T>
{
    public Task<Response<IList<TResp>>> GetAllByUser(Pagination pagination, Guid userId, bool onlyusers);
    public Task<Response<IList<TResp>>> GetAll(Pagination? pagination);
    public Task<Response<TResp>> Get(Guid id);
    public Task<Response<Guid>> Add(TDto model);
    public Task<Response<TResp>> Update(TDto model);
    public Task<Response<bool>> Delete(Guid id);
}

public class BaseService<TReq, TRes, TRepository, T> : IBaseService<TReq, TRes, T>
    where TRepository : class, IBaseRepository<T> where T : IMinBase
{
    protected readonly IUnitOfWork UnitOfWork;
    protected readonly TRepository Repository;

    protected BaseService(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
        Repository = UnitOfWork.GetRepository<TRepository>();
    }

    public async virtual Task<Response<IList<TRes>>> GetAllByUser(Pagination pagination, Guid userId, bool onlyusers = true)
    {
        try
        {
            var (pag, data) = await Repository.GetAllByUser(pagination, onlyusers, userId);

            var res = data.Adapt<List<TRes>>();

            return new Response<IList<TRes>>
            {
                Data = res,
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<TRes>>
            {
                StatusMessage = e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
    public async virtual Task<Response<IList<TRes>>> GetAll(Pagination? paginate)
    {
        try
        {
            var pagination = paginate ?? new Pagination();
            //TODO: Get Pagination from the Query

            var (pag, data) = await Repository.GetAll(pagination);
            return new Response<IList<TRes>>
            {
                Data = data.Adapt<List<TRes>>(),
                Misc = pag,
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<IList<TRes>>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async virtual Task<Response<TRes>> Get(Guid id)
    {
        try
        {
            var entity = await Repository.Get(id, null);
            if (entity == null)
            {
                return new Response<TRes>
                {
                    StatusMessage = $"{typeof(T).Name} Not found",
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            return new Response<TRes>
            {
                Data = entity.Adapt<TRes>(),
                StatusMessage = "Fetch successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<TRes>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async virtual Task<Response<Guid>> Add(TReq reqModel)
    {
        try
        {
            var entity = reqModel.Adapt<T>();
            var entityAsBase = entity as IMinBase ?? 
                throw new InvalidOperationException(
                    "Conversion to IMinBase Failed. Make sure there's Id and CreatedDate properties.");
            
            // Add entity to repository
            var addedEntity = await Repository.Add((T)entityAsBase);
            
            // Save changes to database
            await UnitOfWork.SaveAsync();
            
            // Get the actual ID after save - the tracked entity should have the correct ID
            // Access through IMinBase to get the base class Id property
            var savedId = ((IMinBase)addedEntity).Id;
            
            // If still empty, try to reload from database
            if (savedId == Guid.Empty)
            {
                // Reload the entity to get database-generated values
                await UnitOfWork._context.Entry(addedEntity).ReloadAsync();
                savedId = ((IMinBase)addedEntity).Id;
            }
            
            return new Response<Guid>
            {
                Data = savedId,
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

    public async virtual Task<Response<TRes>> Update(TReq reqModel)
    {
        try
        {
            var entity = reqModel.Adapt<T>();
            var res = await Repository.Update((T)(entity as IMinBase ??
                                       throw new InvalidOperationException(
                                             "Conversion to IMinBase Failed. Make sure there's Id and CreatedDate properties."))
                                             , null);

            await UnitOfWork.SaveAsync();
            return new Response<TRes>
            {
                Data = res.Adapt<TRes>(),
                StatusMessage = "Updated successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<TRes>
            {
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async virtual Task<Response<bool>> Delete(Guid id)
    {
        try
        {
            var res = await Repository.Delete(id);
            await UnitOfWork._context.SaveChangesAsync();
            return new Response<bool>
            {
                Data = res,
                StatusMessage = "Deleted successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<bool>
            {
                Data = false,
                StatusMessage = e.InnerException != null ? e.InnerException.Message : e.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}
