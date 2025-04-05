using Application.Abstractions;
using Application.DTOs.Book;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Book;

public class BookService(IBookRepository bookRepository) : IBookService
{
    public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
    {
        var books = await bookRepository.GetAllAsync().ToListAsync();
        return books.Select(book => new BookDto(book));
    }


    public async Task<long> AddBookAsync(BookDto bookDto)
    {
        var book = bookDto.MapToEntity();
        return await bookRepository.AddAsync(book);
    }

    public async Task<bool> DeleteBookAsync(long id)
    {
        var book = await bookRepository.GetByIdAsync(id);
        if (book == null) return false;
        
        await bookRepository.DeleteAsync(book);
        return true;
    }
}