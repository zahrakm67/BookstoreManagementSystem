using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Book;

public record CreateBookDTO
{
    [MaxLength(500)] public required string Title { get; init; }
    public required string ISBN { get; set; }
    public int? PublisherId { get; set; }
    public DateTime? PublicationDate { get; set; }
    public decimal? Price { get; set; }
    public string Description { get; set; }
    public int? StockQuantity { get; set; }
}