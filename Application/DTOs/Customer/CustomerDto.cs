namespace Application.DTOs.Customer;


public class CustomerDto
{
    public int Id { get; set; }               
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }

    public CustomerDto()
    {
    }

    public CustomerDto(Domain.Entities.Customer? entity)
    {
        if (entity == null) return;
        Id = entity.Id;
        FirstName = entity.FirstName;
        LastName = entity.LastName;
        Email = entity.Email;
        Phone = entity.Phone;
        Address = entity.Address;
    }

    public Domain.Entities.Customer MapToEntity(Domain.Entities.Customer? existingEntity = null)
    {
        var entity = existingEntity ?? new Domain.Entities.Customer();
        entity.Id = Id;
        entity.FirstName = FirstName;
        entity.LastName = LastName;
        entity.Email = Email;
        entity.Phone = Phone;
        entity.Address = Address;
        return entity;
    }
}
