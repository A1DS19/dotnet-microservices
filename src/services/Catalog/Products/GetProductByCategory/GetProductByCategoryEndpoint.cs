namespace Catalog.API.Products.GetProductByCategory;

public record GetProductsByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/products/category/{category}",
                async (string category, ISender sender) =>
                {
                    var response = await sender.Send(new GetProductByCategoryQuery(category));
                    return Results.Ok(response.Products);
                }
            )
            .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Gets a product by category")
            .WithDescription("Gets a product from the catalog by its category.");
    }
}
