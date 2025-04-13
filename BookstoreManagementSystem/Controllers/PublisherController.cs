using Application.Abstractions;
using Application.DTOs.Publisher;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreManagementSystem.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PublisherController(IPublisherService publisherService,ILogger<BooksController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPublishers()
    {
        var books = await publisherService.GetAllPublishersAsync();
        return Ok(books);
    }

    [HttpPost]
    public async Task<IActionResult> AddPublisher(PublisherDto publisherDto)
    {
        var books = await publisherService.AddPublisherAsync(publisherDto);
        return Ok(books);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeletePublisher(long id)
    {
        var result = await publisherService.DeletePublisherAsync(id);
        if (result)
            return Ok();
        return NotFound();
    }
}