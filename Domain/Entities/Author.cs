namespace Domain.Entities;

public partial class Author
{
    public int AuthorId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Biography { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
