using IMS.Domain.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace IMS.DataAccess.UnitOfWork;

public interface IUnitOfWork
{
    Task SaveAsync();
    //(string, Guid) GetRoleAndUserId();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitTransactionAsync(IDbContextTransaction? transaction = null);
    Task RollBackTransactionAsync(IDbContextTransaction? transaction = null);
    T GetRepository<T>() where T : class;
    ApplicationDbContext _context { get; }
}

public class UnitOfWork : IUnitOfWork
{
    //private readonly IHttpContextAccessor _httpContextAccessor;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        // _httpContextAccessor = contextAccessor;
    }

    public ApplicationDbContext _context { get; }

    //public (string, Guid) GetRoleAndUserId()
    //{
    //    return ((string, Guid))_httpContextAccessor.HttpContext?.GetRoleAndId()!;
    //}

    public T GetRepository<T>() where T : class
    {
        var result = Activator.CreateInstance(typeof(T), _context);
        return result as T ?? throw new InvalidOperationException("This Error shouldn't Arise!");
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync(IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            await transaction.CommitAsync();
        }
        else
        {
            await _context.Database.CommitTransactionAsync();
        }
    }

    public async Task RollBackTransactionAsync(IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            await transaction.RollbackAsync();
        }
        else
        {
            await _context.Database.RollbackTransactionAsync();
        }

    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    private bool _disposed;

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
    }
}