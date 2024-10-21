namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

internal class GetProductByQueryIdHandler(
    IDocumentSession session,
    ILogger<GetProductByQueryIdHandler> logger
) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(
        GetProductByIdQuery query,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("GetProductByIdHandler.Handle called with query: {query}", query);

        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null)
        {
            logger.LogWarning(
                "GetProductByIdHandler.Handle found no product with ID {id}",
                query.Id
            );
            throw new ProductNotFoundExceptions(query.Id.ToString());
        }

        logger.LogInformation("GetProductByIdHandler.Handle returning product: {product}", product);
        return new GetProductByIdResult(product);
    }
}
