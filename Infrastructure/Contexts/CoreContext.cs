using Domain.Entities;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Contexts;

public class  CoreContext(DbContextOptions<CoreContext> options)
    : IdentityDbContext<ApplicationUser>(options)
             //CoreContext(DbContextOptions<CoreContext> options) : DbContext(options)
{
    // DbSets for your entities
    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id);
    
            entity.Property(e => e.Id);
            entity.Property(e => e.Biography).HasColumnType("text");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });
    
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id);
    
            entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EA4793451D").IsUnique();
    
            entity.Property(e => e.Id);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PublisherId);
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
    
            entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .HasConstraintName("FK__Book__Publisher");
    
            entity.HasMany(d => d.Authors).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BookAuthor__Author"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BookAuthor__Book"),
                    j =>
                    {
                        j.HasKey("BookId", "AuthorId");
                        j.ToTable("BookAuthors");
                        j.IndexerProperty<int>("BookId");
                        j.IndexerProperty<int>("AuthorId");
                    });
        });
    
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
    
            entity.HasIndex(e => e.Email, "UQ__Customer__A9D10534EFFD553C").IsUnique();
    
            entity.Property(e => e.Id);
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });
    
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
    
            entity.Property(e => e.Id);
            entity.Property(e => e.CustomerId);
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
    
            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Order__Customer");
        });
    
        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id);
    
            entity.Property(e => e.Id);
            entity.Property(e => e.BookId);
            entity.Property(e => e.OrderId);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");
    
            entity.HasOne(d => d.Book).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__OrderDetail__Book");
    
            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDetail__Order");
        });
    
        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.Id);
    
            entity.Property(e => e.Id);
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });
    }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "BookstoreManagementSystem");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            
            var connectionString = configuration.GetConnectionString("CoreDatabase");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}