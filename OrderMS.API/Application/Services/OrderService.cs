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

    public async Task<PaginationDTO<OrderDTO>> GetAllByCustomerId(long clientId, int page = 1, int quantity = 10)
    {
        var all = _ordersCollection
            .Find(o => o.ClientId == clientId);

        var totalResults = await all.CountDocumentsAsync();
        var totalPages = (long)Math.Ceiling(totalResults / (decimal)quantity);

        var orders = await all
            .Skip((page - 1) * quantity)
            .Limit(quantity)
            .ToListAsync();

        return new PaginationDTO<OrderDTO>(page, totalResults, totalPages, orders.Select(OrderDTO.FromEntity));
    }
}
