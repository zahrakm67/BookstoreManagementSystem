using Application.Abstractions;
using Application.DTOs.Publisher;
using Domain.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Publisher;

   
    public class PublisherService(IPublisherRepository publisherRepository) : IPublisherService
    {
        public async Task<IEnumerable<PublisherDto>> GetAllPublishersAsync()
        {
            var publishers = await publisherRepository.GetAllAsync().ToListAsync();
            return publishers.Select(publisher => new PublisherDto(publisher));
        }


        public async Task<long> AddPublisherAsync(PublisherDto publisherDto)
        {
            var publisher = publisherDto.MapToEntity();
            return await publisherRepository.AddAsync(publisher);
        }

        public async Task<bool> DeletePublisherAsync(long id)
        {
            var publisher = await publisherRepository.GetByIdAsync(id);
            if (publisher == null) return false;
        
            await publisherRepository.DeleteAsync(publisher);
            return true;
        }
    }