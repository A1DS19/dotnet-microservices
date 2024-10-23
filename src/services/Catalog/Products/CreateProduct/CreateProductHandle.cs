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

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Categories).NotEmpty().WithMessage("Categories is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
    }
}

internal class CreateProductHandle(IDocumentSession session, ILogger<CreateProductHandle> logger)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Creating product with name {Name}", command.Name);

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

        logger.LogInformation("Product with Id {Id} created", command.Id);

        return new CreateProductResult(product.Id);
    }
}
