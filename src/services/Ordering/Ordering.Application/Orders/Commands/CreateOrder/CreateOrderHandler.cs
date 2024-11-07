namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(
        CreateOrderCommand command,
        CancellationToken cancellationToken
    )
    {
        var order = CreateNewOrder(command.Order);
        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateOrderResult(order.Id.Value);
    }

    private Order CreateNewOrder(OrderDto orderDto)
    {
        var shippingAddress = Address.Of(
            orderDto.ShippingAddress.Street,
            orderDto.ShippingAddress.City,
            orderDto.ShippingAddress.State,
            orderDto.ShippingAddress.Country,
            orderDto.ShippingAddress.ZipCode
        );

        var billingAddress = Address.Of(
            orderDto.BillingAddress.Street,
            orderDto.BillingAddress.City,
            orderDto.BillingAddress.State,
            orderDto.BillingAddress.Country,
            orderDto.BillingAddress.ZipCode
        );

        var order = Order.Create(
            Id: OrderId.Of(Guid.NewGuid()),
            CustomerId: CustomerId.Of(orderDto.CustomerId),
            OrderName: OrderName.Of(orderDto.OrderName),
            ShippingAddress: shippingAddress,
            BillingAddress: billingAddress,
            Payment: Payment.Of(
                orderDto.Payment.CardNumber,
                orderDto.Payment.CardHolderName,
                orderDto.Payment.Expiration,
                orderDto.Payment.SecurityNumber
            )
        );

        foreach (var orderItem in orderDto.OrderItems)
        {
            order.Add(ProductId.Of(orderItem.ProductId), orderItem.Quantity, orderItem.Price);
        }

        return order;
    }
}
