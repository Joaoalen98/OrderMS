using OrderMS.API.Application.DTOs;

namespace OrderMS.API.Domain.Models;

public class OrderItem
{
    public required string Product { get; set; }
    public required int Quantity { get; set; }
    public required double Price { get; set; }

    public static OrderItem FromDTO(OrderItemDTO orderItemDTO)
    {
        return new OrderItem
        {
            Price = orderItemDTO.Price,
            Product = orderItemDTO.Product,
            Quantity = orderItemDTO.Quantity
        };
    }
}
