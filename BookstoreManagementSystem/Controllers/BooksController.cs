using Application.Abstractions;
using Application.DTOs.Book;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreManagementSystem.Controllers;
// [Authorize]
[Route("api/[controller]")]
[ApiController]
public class BooksController(IBookService bookService,ILogger<BooksController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var books = await bookService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(BookDto bookDto)
    {
        var books = await bookService.AddBookAsync(bookDto);
        return Ok(books);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteBook(long id)
    {
        var result = await bookService.DeleteBookAsync(id);
        if (result)
            return Ok();
        return NotFound();
    }
}