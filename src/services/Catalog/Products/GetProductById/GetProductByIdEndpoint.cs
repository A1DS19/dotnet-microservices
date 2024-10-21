namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResponse(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/products/{id}",
                async (ISender sender, Guid id) =>
                {
                    var result = await sender.Send(new GetProductByIdQuery(id));
                    var response = result.Adapt<GetProductByIdResponse>();
                    return Results.Ok(response);
                }
            )
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Gets a product by ID")
            .WithDescription("Gets a product from the catalog by its ID.");
    }
}
