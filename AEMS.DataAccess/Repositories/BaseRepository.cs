using IMS.Domain.Utilities.Exceptions;
using IMS.Domain.Base;
using IMS.Domain.Context;
using IMS.Domain.Utilities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using IMS.Domain.Base;
using IMS.Domain.Context;
using IMS.Domain.Utilities.Exceptions;
using IMS.Domain.Utilities;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;
using System;
using System.Collections.Generic;

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

    public Task<(Pagination, IList<TEntity>)> GetAllByUser(
        Pagination pagination, bool onlyUsers = false,
    Guid? userId = null,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null,

        Func<IQueryable<TEntity>, Guid, IQueryable<TEntity>>? userFilterFunc = null
    );
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

    // Helper to auto-include navigation properties for the entity type
    protected virtual IQueryable<TEntity> ApplyAutoIncludes(IQueryable<TEntity> query)
    {
        var entityType = DbContext.Model.FindEntityType(typeof(TEntity));
        if (entityType == null)
            return query;

        foreach (var navigation in entityType.GetNavigations())
        {
            try
            {
                query = query.Include(navigation.Name);
            }
            catch
            {
                // ignore include failures for safety
            }
        }

        return query;
    }

    public async Task<TEntity> Add(TEntity model)
    {
        // If Id is not set, generate it (EF Core ValueGeneratedOnAdd for GUIDs requires client-side generation)
        if (model.Id == Guid.Empty)
        {
            model.Id = Guid.NewGuid();
        }
        
        model.CreatedDateTime = DateTime.UtcNow;
        model.IsActive = true;
        model.IsDeleted = false;
        
        // Add to DbSet
        var entry = await DbSet.AddAsync(model);
        
        // Return the tracked entity
        return entry.Entity;
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
        var baseQuery = includeFunc?.Invoke(DbSet) ?? ApplyAutoIncludes(DbSet);
        return await baseQuery.FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == id);
    }

    public async Task<(Pagination, IList<TEntity>)> GetAll(Pagination pagination,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc)
    {
        var total = 0;
        var totalPages = 0;

        var query = includeFunc?.Invoke(DbSet) ?? ApplyAutoIncludes(DbSet);

        var res = await query.Where(f => f.IsDeleted != true)
            .Paginate((int)pagination.PageIndex, (int)pagination.PageSize, ref total, ref totalPages).ToListAsync();

        pagination = pagination.Combine(total, totalPages);
        return (pagination, res);
    }

    public async Task<(Pagination, IList<TEntity>)> GetAllByUser(
    Pagination pagination, bool onlyUsers = false,
    Guid? userId = null,
    Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null,

    Func<IQueryable<TEntity>, Guid, IQueryable<TEntity>>? userFilterFunc = null)
    {
        var total = 0;
        var totalPages = 0;

        IQueryable<TEntity> query = includeFunc?.Invoke(DbSet) ?? ApplyAutoIncludes(DbSet);

        query = query.Where(f => f.IsDeleted != true);

        // Apply user filtering if onlyUsers is true and userFilterFunc is provided
        if (onlyUsers && userId.HasValue && userFilterFunc != null)
        {
            query = userFilterFunc(query, userId.Value);
        }

        var res = await query
            .Paginate((int)pagination.PageIndex, (int)pagination.PageSize, ref total, ref totalPages)
            .ToListAsync();

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

        // Skip identity columns to prevent "Cannot update identity column" error
        var entry = DbContext.Entry(found);
        foreach (var property in entry.Properties)
        {
            try
            {
                // Get property from the actual entity type (not interface)
                var propertyInfo = entry.Metadata.ClrType.GetProperty(property.Metadata.Name);
                if (propertyInfo != null)
                {
                    var databaseGeneratedAttr = propertyInfo.GetCustomAttributes(typeof(DatabaseGeneratedAttribute), true)
                        .FirstOrDefault() as DatabaseGeneratedAttribute;
                    
                    // If property is marked as Identity or Computed, don't update it
                    if (databaseGeneratedAttr != null && 
                        (databaseGeneratedAttr.DatabaseGeneratedOption == DatabaseGeneratedOption.Identity ||
                         databaseGeneratedAttr.DatabaseGeneratedOption == DatabaseGeneratedOption.Computed))
                    {
                        property.IsModified = false;
                    }
                }
            }
            catch
            {
                // Skip if ambiguous match or any other reflection error
                continue;
            }
        }

        return found;
    }

    public void Dispose()
    {
        DbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}
