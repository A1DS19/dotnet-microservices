namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Categories,
    string Description,
    string ImageFile,
    decimal Price
) : ICommand<UpdateProductResult>;

public record UpdateProductResult(Guid Id);

internal class UpdateProductCommandHandler(
    IDocumentSession session,
    ILogger<UpdateProductCommandHandler> logger
) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(
        UpdateProductCommand command,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Updating product {Id}", command.Id);

        var product = session.Load<Product>(command.Id);

        if (product is null)
        {
            throw new ProductNotFoundException(command.Id.ToString());
        }

        product.Name = command.Name;
        product.Categories = command.Categories;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(product.Id);
    }
}
