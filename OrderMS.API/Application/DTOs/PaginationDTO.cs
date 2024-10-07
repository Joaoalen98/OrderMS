namespace OrderMS.API.Application.DTOs;

public class PaginationDTO<T>(int page, long totalElements, long totalPages, IEnumerable<T> results)
{
    public int Page { get; } = page;
    public int Size { get; } = results.Count();
    public long TotalElements { get; set; } = totalElements;
    public long TotalPages { get; set; } = totalPages;
    public IEnumerable<T> Results { get; } = results;
}
