using Domain.Entities;

namespace Domain.Abstractions;

public interface IBookRepository
{
    IQueryable<Book?> GetAllAsync();
    Task<Book?> GetByIdAsync(long id);
    Task<long> AddAsync(Book book);
    Task DeleteAsync(Book book);
}