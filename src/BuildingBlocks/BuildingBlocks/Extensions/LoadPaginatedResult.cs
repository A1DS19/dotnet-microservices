using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Extensions;

public static class LoadPaginatedResult
{
    public static Task<List<T>> ToPaginatedListAsync<T>(
        this IQueryable<T> query,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    ) => query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken);
}
