using Domain.Entities;

namespace Domain.Repository.Abstractions;

public interface IPublisherRepository
{
    IQueryable<Publisher?> GetAllAsync();
    Task<Publisher?> GetByIdAsync(long id);
    Task<long> AddAsync(Publisher publisher);
    Task DeleteAsync(Publisher publisher);
}