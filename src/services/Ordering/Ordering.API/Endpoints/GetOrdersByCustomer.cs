using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints;

// public record GetOrdersByCustomerRequest(Guid CustomerId);
public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/orders/customer/{customerId:guid}",
                async (Guid customerId, ISender sender) =>
                {
                    var result = await sender.Send(new GetOrdersByCustomerQuery(customerId));
                    var response = result.Adapt<GetOrdersByCustomerResponse>();
                    return Results.Ok(response);
                }
            )
            .WithName("GetOrdersByCustomer")
            .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get orders by customer")
            .WithDescription("Get orders by customer.");
    }
}
