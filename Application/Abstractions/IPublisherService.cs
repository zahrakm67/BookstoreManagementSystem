using Application.DTOs.Publisher;

namespace Application.Abstractions;

public interface IPublisherService
{
    Task<IEnumerable<PublisherDto>>   GetAllPublishersAsync();
    Task<long>   AddPublisherAsync(PublisherDto publisherDto);
    Task<bool>   DeletePublisherAsync(long id);
}