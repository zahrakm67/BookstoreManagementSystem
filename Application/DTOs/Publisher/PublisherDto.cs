namespace Application.DTOs.Publisher;
public class PublisherDto
{
    public int Id { get; set; }           
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public PublisherDto()
    {
    }

    public PublisherDto(Domain.Entities.Publisher? entity)
    {
        if (entity == null) return;
        Id = entity.Id;
        Name = entity.Name;
        Address = entity.Address;
        Phone = entity.Phone;
        Email = entity.Email;
    }

    public Domain.Entities.Publisher MapToEntity(Domain.Entities.Publisher? existingEntity = null)
    {
        var entity = existingEntity ?? new Domain.Entities.Publisher();
        entity.Id = Id;
        entity.Name = Name;
        entity.Address = Address;
        entity.Phone = Phone;
        entity.Email = Email;
        return entity;
    }
}