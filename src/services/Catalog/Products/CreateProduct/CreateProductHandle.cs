using MediatR;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
    Guid Id,
    string Name,
    List<string> Categories,
    string Description,
    string ImageFile,
    decimal Price
) : IRequest<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductHandle : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    // Handle business logic for creating a product
    public Task<CreateProductResult> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}
