using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints;

// public record DeleteOrderRequest(Guid OrderId);

public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(
                "/orders/{OrderId:guid}",
                async (Guid OrderId, ISender sender) =>
                {
                    var result = await sender.Send(new DeleteOrderCommand(OrderId));
                    var response = result.Adapt<DeleteOrderResponse>();

                    return Results.Ok(response);
                }
            )
            .WithName("DeleteOrder")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete a order")
            .WithDescription("Deletes a order with the provided OrderId.");
    }
}
