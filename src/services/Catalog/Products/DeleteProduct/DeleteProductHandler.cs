namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool isSuccess);

public class DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(
        DeleteProductCommand command,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Deleting product with ID {Id}", command.Id);

        session.Delete<Product>(command.Id);

        logger.LogInformation("Product with ID {Id} was deleted", command.Id);

        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}
