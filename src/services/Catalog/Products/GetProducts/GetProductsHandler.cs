namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(
    IDocumentSession session,
    ILogger<GetProductsQueryHandler> logger
) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(
        GetProductsQuery query,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("GetProductsQueryHandler.Handle called with query: {query}", query);

        var products = await session.Query<Product>().ToListAsync(cancellationToken);

        logger.LogInformation(
            "GetProductsQueryHandler.Handle returning {count} products",
            products.Count
        );

        return new GetProductsResult(products);
    }
}