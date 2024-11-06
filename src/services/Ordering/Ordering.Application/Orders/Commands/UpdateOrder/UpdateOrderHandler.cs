namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(
        UpdateOrderCommand command,
        CancellationToken cancellationToken
    )
    {
        var orderId = OrderId.Of(command.Order.Id);
        var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Order.Id);
        }

        UpdateOrderWithNewValues(order, command.Order);
        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new UpdateOrderResult(true);
    }

    private void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
    {
        order.Update(
            orderName: OrderName.Of(orderDto.OrderName),
            shippingAddress: Address.Of(
                orderDto.ShippingAddress.Street,
                orderDto.ShippingAddress.City,
                orderDto.ShippingAddress.State,
                orderDto.ShippingAddress.Country,
                orderDto.ShippingAddress.ZipCode
            ),
            billingAddress: Address.Of(
                orderDto.BillingAddress.Street,
                orderDto.BillingAddress.City,
                orderDto.BillingAddress.State,
                orderDto.BillingAddress.Country,
                orderDto.BillingAddress.ZipCode
            ),
            payment: Payment.Of(
                orderDto.Payment.CardNumber,
                orderDto.Payment.CardHolderName,
                orderDto.Payment.Expiration,
                orderDto.Payment.SecurityNumber
            )
        );
    }
}
