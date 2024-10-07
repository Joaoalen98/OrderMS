using OrderMS.API.Domain.Models;

namespace OrderMS.API.Application.DTOs;

public record class OrderItemDTO(
    string Product,
    int Quantity,
    double Price
)
{
    public static OrderItemDTO FromEntity(OrderItem orderItem)
    {
        return new OrderItemDTO(
            orderItem.Product,
            orderItem.Quantity,
            orderItem.Price);
    }
}
