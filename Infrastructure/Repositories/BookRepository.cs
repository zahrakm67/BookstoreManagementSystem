using Domain.Entities;
using Domain.Abstractions;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BookRepository(CoreContext coreContext) : IBookRepository
{
    public IQueryable<Book?> GetAllAsync()
    {
        return  coreContext.Books;
    }

    public async Task<Book?> GetByIdAsync(long id)
    {
        return await coreContext.Books.FirstOrDefaultAsync(b=> b.BookId==id);
    }

    public async Task<long> AddAsync(Book book)
    {
        await coreContext.Books.AddAsync(book, CancellationToken.None);
        await coreContext.SaveChangesAsync(CancellationToken.None);
        return book.BookId;
    }

    public async Task DeleteAsync(Book book)
    {
        coreContext.Books.Remove(book);
        await coreContext.SaveChangesAsync(CancellationToken.None);
    }
}
