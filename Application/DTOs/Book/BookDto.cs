namespace Application.DTOs.Book;

public class BookDto
{
    public int Id { get; set; }                // Maps to BookID
    public string Title { get; set; }
    public string ISBN { get; set; }
    public int? PublisherId { get; set; }
    public DateTime? PublicationDate { get; set; }
    public decimal? Price { get; set; }
    public string Description { get; set; }
    public int? StockQuantity { get; set; }

    public BookDto()
    {
    }

    public BookDto(Domain.Entities.Book? entity)
    {
        if (entity == null) return;
        Id = entity.Id;
        Title = entity.Title;
        ISBN = entity.Isbn;
        PublisherId = entity.PublisherId;
        PublicationDate = entity.PublicationDate;
        Price = entity.Price;
        Description = entity.Description;
        StockQuantity = entity.StockQuantity;
    }

    public Domain.Entities.Book MapToEntity(Domain.Entities.Book? existingEntity = null)
    {
        var entity = existingEntity ?? new Domain.Entities.Book();
        entity.Id = Id;
        entity.Title = Title;
        entity.Isbn = ISBN;
        entity.PublisherId = PublisherId;
        entity.PublicationDate = PublicationDate;
        entity.Price = Price;
        entity.Description = Description;
        entity.StockQuantity = StockQuantity;
        return entity;
    }
}
