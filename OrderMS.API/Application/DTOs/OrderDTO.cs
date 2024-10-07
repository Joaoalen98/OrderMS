using OrderMS.API.Domain.Models;

namespace OrderMS.API.Application.DTOs;

public class OrderDTO(long code, long clientId, IEnumerable<OrderItemDTO> items)
{
    public long Code { get; } = code;
    public long ClientId { get; } = clientId;
    public IEnumerable<OrderItemDTO> Items { get; } = items;
    public double Total { get; } = items.Sum(x => x.Price * x.Quantity);

    public static OrderDTO FromEntity(Order order)
    {
        return new OrderDTO(
            order.Code,
            order.ClientId,
            order.Items.Select(OrderItemDTO.FromEntity));
    }
}
