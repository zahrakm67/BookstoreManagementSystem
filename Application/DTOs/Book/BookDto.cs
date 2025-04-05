namespace Application.DTOs.Book;

public class BookDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }

    public BookDto()
    {
    }


    public Domain.Entities.Book MapToEntity(Domain.Entities.Book? existingEntity = null)
    {
        var entity = existingEntity ?? new Domain.Entities.Book();
        entity.Id = Id;
        entity.Title = Title;
        entity.Author = Author;

        return entity;
    }

    public BookDto(Domain.Entities.Book? entity = null)
    {
        if (entity == null) return;

        Id = entity.Id;
        Title = entity.Title;
        Author = entity.Author;
    }
}