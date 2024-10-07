using OrderMS.API.Application.DTOs;

namespace OrderMS.API.Application.Interfaces;

public interface IOrderService
{
    Task<OrderDTO> Create(OrderDTO order);
    Task<PaginationDTO<OrderDTO>> GetAllByCustomerId(long clientId, int page = 1, int quantity = 10);
}
