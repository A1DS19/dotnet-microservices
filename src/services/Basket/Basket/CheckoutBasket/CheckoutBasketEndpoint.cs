namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketRequest(CheckoutBasketDto BasketDto);

public record CheckoutBasketResponse(bool IsSuccess);

public class CheckoutBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/basket/checkout",
                async (CheckoutBasketDto request, ISender sender) =>
                {
                    var command = request.Adapt<CheckoutBasketCommand>();
                    var result = await sender.Send(command);
                    var response = result.Adapt<CheckoutBasketResponse>();

                    return Results.Created("/basket/checkout", response);
                }
            )
            .WithName("CreateBasket")
            .Produces<CheckoutBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create a basket")
            .WithDescription("Creates a basket with the provided items.");
    }
}
