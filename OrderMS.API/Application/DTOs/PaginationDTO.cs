namespace OrderMS.API.Application.DTOs;

public class PaginationDTO<T>(int page, IEnumerable<T> results)
{
    public int Page { get; } = page;
    public int Quantity { get; } = results.Count();
    public IEnumerable<T> Results { get; } = results;
}
