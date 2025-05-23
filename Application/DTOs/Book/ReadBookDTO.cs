namespace Application.DTOs.Book;

public record ReadBookDTO
{
    public int Id { get; set; }                // Maps to BookID
    public string Title { get; set; }
    public string ISBN { get; set; }
    public string Publisher { get; set; }
    public DateTime? PublicationDate { get; set; }
    public decimal? Price { get; set; }
    public string Description { get; set; }
    public int? StockQuantity { get; set; }
    
    public ReadBookDTO(Domain.Entities.Book? entity)
    {
        if (entity == null) return;
        Id = entity.Id;
        Title = entity.Title;
        ISBN = entity.Isbn;
        Publisher = entity.Publisher.Name;
        PublicationDate = entity.PublicationDate;
        Price = entity.Price;
        Description = entity.Description;
        StockQuantity = entity.StockQuantity;
    }
}