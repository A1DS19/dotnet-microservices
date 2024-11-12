namespace Ordering.Application.EventHandlers.Domain;

public class OrderCreatedEventHandler(
    IPublishEndpoint publish,
    IFeatureManager featureManager,
    ILogger<OrderCreatedEventHandler> logger
) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Domain Event {domainEvent.GetType().Name} handled.");

        if (await featureManager.IsEnabledAsync("OrderFulfillment"))
        {
            var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();
            await publish.Publish(orderCreatedIntegrationEvent, cancellationToken);
        }
    }
}
