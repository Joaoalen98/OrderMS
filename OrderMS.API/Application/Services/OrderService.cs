using MongoDB.Driver;
using OrderMS.API.Application.DTOs;
using OrderMS.API.Application.Interfaces;
using OrderMS.API.Domain.Models;

namespace OrderMS.API.Application.Services;

public class OrderService(IMongoDatabase mongoDatabase) : IOrderService
{
    private readonly IMongoCollection<Order> _ordersCollection = mongoDatabase.GetCollection<Order>("orders");

    public async Task<OrderDTO> Create(OrderDTO orderDTO)
    {
        await _ordersCollection.InsertOneAsync(Order.FromDTO(orderDTO));
        return orderDTO;
    }

    public async Task<PaginationDTO<OrderDTO>> GetAll(int page = 1, int quantity = 10)
    {
        var orders = await _ordersCollection
            .Find(_ => true)
            .Skip((page - 1) * quantity)
            .Limit(quantity)
            .ToListAsync();

        return new PaginationDTO<OrderDTO>(page, orders.Select(OrderDTO.FromEntity));
    }
}
