namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketRequest(CheckoutBasketDto BasketCheckoutDto);

public record CheckoutBasketResponse(bool IsSuccess);

public class CheckoutBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/basket/checkout",
                async (CheckoutBasketRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CheckoutBasketCommand>();
                    var result = await sender.Send(command);
                    var response = result.Adapt<CheckoutBasketResponse>();

                    return Results.Created("/basket/checkout", response);
                }
            )
            .WithName("CheckoutBasket")
            .Produces<CheckoutBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Checkout a basket")
            .WithDescription("Checkout a basket with the provided items.");
    }
}
