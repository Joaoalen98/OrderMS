using OrderMS.API.Application.DTOs;
using OrderMS.API.Domain.Models;

namespace OrderMS.API.Application.Interfaces;

public interface IOrderService
{
    Task<OrderDTO> Create(OrderDTO order);
    Task<IEnumerable<OrderDTO>> GetAll();
}
