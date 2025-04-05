namespace Domain.Entities;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? Isbn { get; set; }

    public int? PublisherId { get; set; }

    public DateOnly? PublicationDate { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public int? StockQuantity { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Publisher? Publisher { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
