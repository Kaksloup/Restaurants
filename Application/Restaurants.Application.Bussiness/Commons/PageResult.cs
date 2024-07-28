namespace Restaurants.Application.Bussiness.Commons;

public class PageResult<T>(List<T> items, int totalCount, int pageSize, int pageNumber) where T : class
{
    public List<T> Items { get; set; } = items;
    public int TotalPages { get; set; } = (int)Math.Ceiling(totalCount / (double)pageSize);
    public int TotalCountItems { get; set; } = totalCount;
    public int ItemFrom { get; set; } = pageSize * (pageNumber - 1) + 1;
    public int ItemTo { get; set; } = pageSize * (pageNumber - 1) + 1 + pageSize - 1;
}