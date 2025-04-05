using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class CoreContext:DbContext
{
    public CoreContext(DbContextOptions<CoreContext> options) : base(options)
    {
    }

    // DbSets for your entities
    public DbSet<Book?> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure entity relationships or constraints here if needed
        modelBuilder.Entity<Book>().HasKey(b => b.Id);
    }
}