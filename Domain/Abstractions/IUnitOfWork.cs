namespace Domain.Abstractions;

public interface IUnitOfWork
{
    Task<long> SaveChangesAsync(CancellationToken cancellationToken = default);
}