using OrderMS.API.Domain.Models;

namespace OrderMS.API.Application.DTOs;

public record class OrderDTO(
    long Code,
    long ClientId,
    IEnumerable<OrderItemDTO> Items
)
{
    public static OrderDTO FromEntity(Order order)
    {
        return new OrderDTO(
            order.Code,
            order.ClientId,
            order.Items.Select(OrderItemDTO.FromEntity));
    }
}
