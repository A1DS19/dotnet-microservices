namespace Ordering.Application.Orders.EventHandlers;

public class OrderCreatedEventHandler(ILogger logger) : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Domain Event {notification.GetType().Name} handled.");

        return Task.CompletedTask;
    }
}
