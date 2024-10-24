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

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MinimumLength(3)
            .WithMessage("Name must be at least 3 characters")
            .MaximumLength(100)
            .WithMessage("Name must not exceed 100 characters");
        RuleFor(x => x.Categories).NotEmpty().WithMessage("Categories is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

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
            logger.LogWarning("Product {Id} was not found", command.Id);
            throw new ProductNotFoundException(command.Id.ToString());
        }

        product.Name = command.Name;
        product.Categories = command.Categories;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        session.Update(product);

        logger.LogInformation("Product {Id} was updated", command.Id);

        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(product.Id);
    }
}
