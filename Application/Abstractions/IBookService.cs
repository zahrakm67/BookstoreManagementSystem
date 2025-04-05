using Application.DTOs.Book;

namespace Application.Abstractions;

public interface IBookService
{
  Task<IEnumerable<BookDto>>   GetAllBooksAsync();
  Task<long>   AddBookAsync(BookDto bookDto);
  Task<bool>   DeleteBookAsync(long id);
}