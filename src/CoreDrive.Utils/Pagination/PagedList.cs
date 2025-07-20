using System.Collections;
using Microsoft.EntityFrameworkCore;
namespace CoreDrive.Utils.Pagination;


public class PagedList<T>(List<T> items, int count, int pageNumber, int pageSize)
    : IReadOnlyList<T>
{
    public MetaData MetaData { get; } = new()
    {
        CurrentPage = pageNumber,
        PageSize = pageSize,
        TotalCount = count,
        TotalPages = (int)Math.Ceiling(count / (double)pageSize)
    };
    
    public T this[int index] => items[index];
    public int Count => items.Count;
    public IEnumerator<T> GetEnumerator() => items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static async Task<PagedList<T>> ToPagedList(
        IQueryable<T> query, 
        int pageNumber, 
        int pageSize)
    {
        int count = await query.CountAsync();
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}