namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductResponse(bool isSuccess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(
                "/products/{productId}",
                async (Guid productId, ISender sender) =>
                {
                    var result = await sender.Send(new DeleteProductCommand(productId));
                    var response = result.Adapt<DeleteProductResponse>();
                    return Results.Ok(response);
                }
            )
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Deletes a product")
            .WithDescription("Deletes a product in the catalog");
    }
}
