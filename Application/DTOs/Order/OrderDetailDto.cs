namespace Application.DTOs.Order;

public class OrderDetailDto
{
    public int Id { get; set; }              
    public int? OrderId { get; set; }
    public int? BookId { get; set; }
    public int? Quantity { get; set; }
    public decimal? UnitPrice { get; set; }

    public OrderDetailDto()
    {
    }

    public OrderDetailDto(Domain.Entities.OrderDetail? entity)
    {
        if (entity == null) return;
        Id = entity.Id;
        OrderId = entity.OrderId;
        BookId = entity.BookId;
        Quantity = entity.Quantity;
        UnitPrice = entity.UnitPrice;
    }

    public Domain.Entities.OrderDetail MapToEntity(Domain.Entities.OrderDetail? existingEntity = null)
    {
        var entity = existingEntity ?? new Domain.Entities.OrderDetail();
        entity.Id= Id;
        entity.OrderId = OrderId;
        entity.BookId = BookId;
        entity.Quantity = Quantity;
        entity.UnitPrice = UnitPrice;
        return entity;
    }
}