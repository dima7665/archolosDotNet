using archolosDotNet.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace archolosDotNet.Models.Extensions;

public static class QueryableExtension
{
    public static async Task<PagedResult<T>> toPagedResultAsync<T>(this IQueryable<T> source, PaginationPayload data)
    {
        var count = await source.CountAsync();

        var items = await source.Skip((data.page - 1) * data.perPage).Take(data.perPage).ToListAsync();

        var pagination = new PaginationData
        {
            currentPage = data.page,
            perPage = data.perPage,
            count = count,
        };

        return new PagedResult<T>
        {
            data = items,
            pagination = pagination,
        };
    }
}
