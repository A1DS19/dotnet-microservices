namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
    Guid Id,
    string Name,
    List<string> Categories,
    string Description,
    string ImageFile,
    decimal Price
) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductHandle(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    // Handle business logic for creating a product
    public async Task<CreateProductResult> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken
    )
    {
        var product = new Product
        {
            Name = command.Name,
            Categories = command.Categories,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
        };

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}
