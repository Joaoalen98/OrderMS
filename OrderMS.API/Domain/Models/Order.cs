using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OrderMS.API.Application.DTOs;

namespace OrderMS.API.Domain.Models;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;

    public required long Code { get; set; }
    public required long CustomerId { get; set; }
    public required IEnumerable<OrderItem> Items { get; set; } = default!;

    public static Order FromDTO(OrderDTO orderDTO)
    {
        return new Order
        {
            CustomerId = orderDTO.CustomerId,
            Code = orderDTO.Code,
            Items = orderDTO.Items.Select(OrderItem.FromDTO)
        };
    }
}
