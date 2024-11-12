using BuildingBlocks.Messaging.Events;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.EventHandlers.Integration;

public class BasketCheckoutEventHandler(ISender sender, ILogger logger)
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation(
            "Integration event received from BasketCheckoutEvent: {IntegrationEventId} - ({@IntegrationEvent})",
            context.Message.Id,
            context.Message
        );

        var command = MapToCreateOrderCommand(context.Message);
        await sender.Send(command);
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        var addressDto = new AddressDto(
            message.Street,
            message.City,
            message.State,
            message.Country,
            message.ZipCode
        );
        var paymentDto = new PaymentDto(
            message.CardNumber,
            message.CardHolderName,
            message.Expiration,
            message.SecurityNumber
        );
        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(
            Id: orderId,
            CustomerId: message.CustomerId,
            OrderName: message.Username,
            ShippingAddress: addressDto,
            BillingAddress: addressDto,
            Payment: paymentDto,
            OrderStatus: OrderStatus.Pending,
            OrderItems:
            [
                new OrderItemDto(orderId, new Guid("C67D6323-E8B1-4BDF-9A75-B0D0D2E7E914"), 2, 400),
            ]
        );

        return new CreateOrderCommand(orderDto);
    }
}
