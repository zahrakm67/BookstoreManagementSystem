namespace Application.DTOs.Book;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; }

    public BookDto()
    {
    }


    public Domain.Entities.Book MapToEntity(Domain.Entities.Book? existingEntity = null)
    {
        var entity = existingEntity ?? new Domain.Entities.Book();
        entity.BookId = Id;
        entity.Title = Title;
        

        return entity;
    }

    public BookDto(Domain.Entities.Book? entity = null)
    {
        if (entity == null) return;

        Id = entity.BookId;
        Title = entity.Title;
    }
}