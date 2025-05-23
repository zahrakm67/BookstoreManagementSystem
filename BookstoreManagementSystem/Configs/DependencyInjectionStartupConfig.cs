using Application.Abstractions;
using Application.Services.Book;
using Application.Services.Publisher;
using Domain;
using Domain.Repository.Abstractions;
using Infrastructure;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookstoreManagementSystem.Configs;

public static class DependencyInjectionStartupConfig
{
    public static void Setup(IServiceCollection services, IConfiguration configuration)
    {
        SetupDbContexts(services, configuration);
        SetupRepositories(services);
        SetupServices(services);
    }
    
    private static void SetupDbContexts(IServiceCollection services, IConfiguration configuration)
    {
    
        services.AddDbContext<CoreContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("CoreDatabase"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Transient);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void SetupRepositories(IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
         services.AddScoped<IPublisherRepository, PublisherRepository>();
    }

    private static void SetupServices(IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IPublisherService, PublisherService>();

    }
}