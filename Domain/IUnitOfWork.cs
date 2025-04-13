using Domain.Repository;

namespace Domain;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> GenericRepository<T>() where T : class;
    void CreateTransaction();
    Task CreateTransactionAsync();
    void Commit();
    Task CommitAsync();
    void Rollback();
    Task RollbackAsync();
    int Save();
    Task<int> SaveAsync(CancellationToken cancellationToken = default);
    void MarkEntryAsAdded<T>(T entity) where T : class;
    void MarkEntryAsModified<T>(T entity) where T : class;
    Task MarkEntryAsModifiedAsync<T>(T entity) where T : class;
    void MarkEntriesAsModified<T>(IEnumerable<T> entities) where T : class;
    Task MarkEntriesAsModifiedAsync<T>(IEnumerable<T> entities) where T : class;
    void MarkEntriesAsDeleted<T>(IEnumerable<T> entities) where T : class;
    Task MarkEntryAsAddedAsync<T>(T entity) where T : class;
    void MarkEntriesAsAdded<T>(IEnumerable<T> entities) where T : class;
    Task MarkEntriesAsAddedAsync<T>(IEnumerable<T> entities) where T : class;
    void MarkEntryAsDeleted<T>(T entity) where T : class;
}