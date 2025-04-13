using Domain;
using Domain.Repository;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class UnitOfWork: IUnitOfWork
{
    private bool _disposed;
    private readonly CoreContext _dbContext;
    private Dictionary<string, object?>? _repos;

    public UnitOfWork(CoreContext dbContext) => _dbContext = dbContext;

    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _dbContext.Dispose();
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public IGenericRepository<TRepository> GenericRepository<TRepository>() where TRepository : class
    {
        _repos ??= new Dictionary<string, object?>();

        var type = typeof(TRepository).Name;
        if (!_repos.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<TRepository>);
            var repositoryInstance = Activator.CreateInstance(repositoryType, _dbContext);
            _repos.Add(type, repositoryInstance);
        }

        return _repos[type] as GenericRepository<TRepository> ??
               throw new InvalidOperationException($"Repository: {type} - not loaded");
    }

    public void CreateTransaction()
    {
        _dbContext.Database.BeginTransaction();
    }

    public async Task CreateTransactionAsync()
    {
        await _dbContext.Database.BeginTransactionAsync();
    }

    public void Commit()
    {
        _dbContext.Database.CommitTransaction();
    }


    public async Task CommitAsync()
    {
        await _dbContext.Database.CommitTransactionAsync();
    }

    public void Rollback()
    {
        _dbContext.Database.RollbackTransaction();
        _dbContext.Dispose();
    }

    public async Task RollbackAsync()
    {
        await _dbContext.Database.RollbackTransactionAsync();
        await _dbContext.DisposeAsync();
    }


    public int Save()
    {
        return _dbContext.SaveChanges();
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void MarkEntryAsAdded<T>(T entity) where T : class
    {
        _dbContext.Entry(entity).State = EntityState.Added;
    }

    public void MarkEntryAsModified<T>(T entity) where T : class
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public void MarkEntriesAsDeleted<T>(IEnumerable<T> entities) where T : class
    {
        foreach (var entity in entities)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }
    }

    public Task MarkEntryAsModifiedAsync<T>(T entity) where T : class
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public void MarkEntriesAsModified<T>(IEnumerable<T> entities) where T : class
    {
        foreach (var entity in entities)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }

    public Task MarkEntriesAsModifiedAsync<T>(IEnumerable<T> entities) where T : class
    {
        foreach (var entity in entities)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        return Task.CompletedTask;
    }

    public Task MarkEntryAsAddedAsync<T>(T entity) where T : class
    {
        _dbContext.Entry(entity).State = EntityState.Added;
        return Task.CompletedTask;
    }

    public void MarkEntriesAsAdded<T>(IEnumerable<T> entities) where T : class
    {
        foreach (var entity in entities)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
        }
    }

    public Task MarkEntriesAsAddedAsync<T>(IEnumerable<T> entities) where T : class
    {
        foreach (var entity in entities)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
        }

        return Task.CompletedTask;
    }

    public void MarkEntryAsDeleted<T>(T entity) where T : class
    {
        _dbContext.Entry(entity).State = EntityState.Deleted;
    }

    public void ClearChanges()
    {
        _dbContext.ChangeTracker.Clear();
    }
}