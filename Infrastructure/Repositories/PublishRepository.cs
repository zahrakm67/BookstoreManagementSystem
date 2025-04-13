using Domain.Entities;
using Domain.Repository.Abstractions;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PublisherRepository(CoreContext coreContext) : IPublisherRepository
{
    public IQueryable<Publisher?> GetAllAsync()
    {
        return  coreContext.Publishers;
    }

    public async Task<Publisher?> GetByIdAsync(long id)
    {
        return await coreContext.Publishers.FirstOrDefaultAsync(b=> b.Id==id);
    }

    public async Task<long> AddAsync(Publisher publisher)
    {
        await coreContext.Publishers.AddAsync(publisher, CancellationToken.None);
        await coreContext.SaveChangesAsync(CancellationToken.None);
        return publisher.Id;
    }

    public async Task DeleteAsync(Publisher publisher)
    {
        coreContext.Publishers.Remove(publisher);
        await coreContext.SaveChangesAsync(CancellationToken.None);
    }
}
