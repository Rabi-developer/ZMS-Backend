﻿using IMS.Domain.Utilities.Exceptions;
using IMS.Domain.Base;
using IMS.Domain.Context;
using IMS.Domain.Utilities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace IMS.DataAccess.Repositories;


public interface IBaseRepository<TEntity> : IDisposable where TEntity : IMinBase
{
    /// <summary>
    /// Get All Entities With applied pagination and also applying any Includes needed for the Particular Entity
    /// </summary>
    /// <param name="pagination"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public Task<(Pagination, IList<TEntity>)> GetAll(Pagination pagination,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? func = null);

    /// <summary>
    /// Get the Entity with Id
    /// </summary>
    /// <param name="id">Valid id for the entity</param>
    /// <returns></returns>
    public Task<TEntity?> Get(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc);

    /// <summary>
    /// Add the Entity to DB
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Task<TEntity> Add(TEntity model);

    /// <summary>
    /// Update the Db Entity. 
    /// </summary>
    /// <param name="model">The Source Model</param>
    /// <param name="func">The Update Function for Mapping Entity Properties</param>
    /// <returns></returns>
    public Task<TEntity> Update(TEntity model, Func<TEntity, TEntity>? func);

    /// <summary>
    /// Delete an Entity with the Specified Id
    /// </summary>
    /// <param name="id">Valid Id of the Entity</param>
    /// <returns></returns>
    public Task<bool> Delete(Guid id);
}

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class, IGeneralBase
{
    protected readonly DbSet<TEntity> DbSet;
    protected readonly ApplicationDbContext DbContext;
    //public readonly HttpContext HttpContext;

    protected BaseRepository(ApplicationDbContext dbContext)//, IHttpContextAccessor httpContextAccessor)
    {
        DbSet = dbContext.Set<TEntity>();
        DbContext = dbContext;
        //HttpContext = httpContextAccessor.HttpContext ??
        //              throw new NotImplementedException(
        //                  "HttpContextAccessor Cannot be Null. Verify whether it's properly Injected or not.");
    }

    public async Task<TEntity> Add(TEntity model)
    {
        model.CreatedDateTime = DateTime.UtcNow;
        model.IsActive = true;
        model.IsDeleted = false;
        await DbSet.AddAsync(model);
        return model;
    }

    public async Task<bool> Delete(Guid id)
    {
        var found = await DbSet
            .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted != true);
        if (found == null)
        {
            throw new EntityNotFoundException($"{nameof(TEntity)} not found for Id:{id}");
        }

        found.IsDeleted = true;
        return true;
    }

    public async Task<TEntity?> Get(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc)
    {
        return await (includeFunc?.Invoke(DbSet) ?? DbSet).FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == id);
    }

    public async Task<(Pagination, IList<TEntity>)> GetAll(Pagination pagination,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc)
    {
        var total = 0;
        var totalPages = 0;

        var res = await (includeFunc?.Invoke(DbSet) ?? DbSet).Where(f => f.IsDeleted != true)
            .Paginate((int)pagination.PageIndex, (int)pagination.PageSize, ref total, ref totalPages).ToListAsync();

        pagination = pagination.Combine(total, totalPages);
        return (pagination, res);
    }

    public async Task<TEntity> Update(TEntity model, Func<TEntity, TEntity>? func)
    {
        var found = await DbSet
            .FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == model.Id);
        if (found == null)
        {
            throw new EntityNotFoundException($"{nameof(TEntity)} not found with Id: {model.Id}");
        }

        if (func != null)
        {
            found = func(found);
        }
        found.ModifiedDateTime = model.ModifiedDateTime;
        //found.CreatedDateTime = found.CreatedDateTime;
        //found.CreatedBy = found.CreatedBy;

        DbContext.Entry(found).CurrentValues.SetValues(model);
        DbContext.Entry(found).State = EntityState.Modified;

        return found;
    }

    public void Dispose()
    {
        DbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}
