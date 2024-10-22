namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResponse>;

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler(
    IDocumentSession session,
    ILogger<GetProductByCategoryQueryHandler> logger
) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResponse>
{
    public async Task<GetProductByCategoryResponse> Handle(
        GetProductByCategoryQuery request,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation(
            "GetProductByCategoryQueryHandler.Handle called with query: {query}",
            request
        );

        var products = await session
            .Query<Product>()
            .Where(p => p.Categories.Contains(request.Category))
            .ToListAsync(cancellationToken);

        logger.LogInformation(
            "GetProductByCategoryQueryHandler.Handle returning {count} products",
            products.Count
        );

        return new GetProductByCategoryResponse(products);
    }
}
