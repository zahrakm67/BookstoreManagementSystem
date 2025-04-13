namespace Application.DTOs.Order;

public class OrderDto
{
    public int Id { get; set; }                // Maps to OrderID
    public int? CustomerId { get; set; }
    public DateTime? OrderDate { get; set; }
    public decimal? TotalAmount { get; set; }
    public string OrderStatus { get; set; }

    public OrderDto()
    {
    }

    public OrderDto(Domain.Entities.Order? entity)
    {
        if (entity == null) return;
        Id = entity.Id;
        CustomerId = entity.CustomerId;
        OrderDate = entity.OrderDate;
        TotalAmount = entity.TotalAmount;
        OrderStatus = entity.OrderStatus;
    }

    public Domain.Entities.Order MapToEntity(Domain.Entities.Order? existingEntity = null)
    {
        var entity = existingEntity ?? new Domain.Entities.Order();
        entity.Id= Id;
        entity.CustomerId = CustomerId;
        entity.OrderDate = OrderDate;
        entity.TotalAmount = TotalAmount;
        entity.OrderStatus = OrderStatus;
        return entity;
    }
}