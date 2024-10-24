namespace Catalog.API.Products.GetProducts;

public record GetProductsRequest(int? PageNumber, int? PageSize) : IQuery<GetProductsResult>;

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/products",
                async ([AsParameters] GetProductsRequest request, ISender sender) =>
                {
                    var query = request.Adapt<GetProductsQuery>();

                    var result = await sender.Send(
                        new GetProductsQuery(query.PageNumber, query.PageSize)
                    );

                    var response = result.Adapt<GetProductsResponse>();
                    return Results.Ok(response);
                }
            )
            .WithName("GetProduct")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Gets all products")
            .WithDescription("Gets all products from the catalog.");
        ;
    }
}
