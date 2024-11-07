namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(
        GetOrdersQuery query,
        CancellationToken cancellationToken
    )
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;
        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

        var orders = await dbContext
            .Orders.Include(o => o.OrderItems)
            .OrderBy(o => o.OrderName.Value)
            .ToPaginatedListAsync(pageIndex, pageSize, cancellationToken);

        return new GetOrdersResult(
            new PaginatedResult<OrderDto>(
                pageSize: pageSize,
                pageIndex: pageIndex,
                count: totalCount,
                data: orders.ToOrderDtoList()
            )
        );
    }
}
