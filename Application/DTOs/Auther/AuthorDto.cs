namespace Application.DTOs.Auther;

public class AuthorDto
{
    public int Id { get; set; }                
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Biography { get; set; }

    public AuthorDto()
    {
    }

    public AuthorDto(Domain.Entities.Author? entity)
    {
        if (entity == null) return;
        Id = entity.Id;
        FirstName = entity.FirstName;
        LastName = entity.LastName;
        Biography = entity.Biography;
    }

    public Domain.Entities.Author MapToEntity(Domain.Entities.Author? existingEntity = null)
    {
        var entity = existingEntity ?? new Domain.Entities.Author();
        entity.Id = Id;
        entity.FirstName = FirstName;
        entity.LastName = LastName;
        entity.Biography = Biography;
        return entity;
    }
}