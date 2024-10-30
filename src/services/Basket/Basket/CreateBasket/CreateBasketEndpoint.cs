namespace Basket.API.Basket.CreateBasket;

public record CreateBasketRequest(ShoppingCart Cart);

public record CreateBasketResponse(string UserName);

public class CreateBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "basket",
                async (CreateBasketRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateBasketCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<CreateBasketResponse>();

                    return Results.Created($"/basket/{response.UserName}", response);
                }
            )
            .WithName("CreateBasket")
            .Produces<CreateBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create a basket")
            .WithDescription("Creates a basket with the provided items.");
    }
}
